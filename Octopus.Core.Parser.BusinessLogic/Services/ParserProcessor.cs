using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Parsers;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Enums;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Parser.BusinessLogic.Interfaces.Services;
using Octopus.Core.Parser.BusinessLogic.Services.Factories;
using Octopus.Core.Parser.BusinessLogic.Services.Parsers;
using Octopus.Core.Parser.BusinessLogic.Services.Parsers.Abstraction;
using Octopus.Core.RabbitMq.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Services
{
    public class ParserProcessor : IParserProcessor
    {
        private ParserFactory _parserFactory;

        private readonly ProcessorConfiguration _processorOptions;

        private readonly IOptions<CsvParserConfiguration> _csvOptions;
        private readonly IOptions<XmlParserConfiguration> _xmlOptions;
        private readonly IOptions<JsonParserConfiguration> _jsonOptions;
        private readonly IDynamicObjectCreateService _dynamicObjectCreateService;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private readonly IMongoRepository _mongoRepository;

        public ParserProcessor(IOptions<CsvParserConfiguration> csvOptions,
            IOptions<XmlParserConfiguration> xmlOptions,
            IOptions<JsonParserConfiguration> jsonOptions,
            IOptions<ProcessorConfiguration> processorOptions,
            IDynamicObjectCreateService dynamicObjectCreateService,
            IRabbitMqPublisher rabbitMqPublisher, 
            IMongoRepository mongoRepository)
        {
            _processorOptions = processorOptions.Value;

            _csvOptions = csvOptions;
            _xmlOptions = xmlOptions;
            _jsonOptions = jsonOptions;

            _parserFactory = RegisterParserFactory();

            _dynamicObjectCreateService = dynamicObjectCreateService;

            _rabbitMqPublisher = rabbitMqPublisher;
            _mongoRepository = mongoRepository;
        }

        public async Task Parse(IEntityDescription request)
        {
            if (request != null)
            {
                var modelDescription = await GetExpectedModelDescription(request.EntityType);

                var inputFile = new FileInfo(request.EntityFilePath);

                var objects = await ParseFile(inputFile, modelDescription);

                var outputEntityDescription = GetOutputEntityDescription(request.EntityType);

                SerializeObjectsToOutputFile(objects, outputEntityDescription.EntityFilePath);

                await _rabbitMqPublisher.SendMessage(JsonSerializer.Serialize(outputEntityDescription));
            }
        }

        public Task StartProcessing()
        {
            return Task.CompletedTask;
        }

        public Task StopProcessing()
        {
            return Task.CompletedTask;
        }

        private async Task<DynamicEntityWithProperties> GetExpectedModelDescription(string modelName)
        {
            var dynamicEntity = await _mongoRepository.GetEntity(modelName);

            if (dynamicEntity == null)
            {
                throw new IncorrectInputDataException(ErrorMessages.NoDescriptionProvided);
            }

            return dynamicEntity;
        }

        private void SerializeObjectsToOutputFile(IEnumerable<object> objects, string outputFilePath)
        {
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };

            var jsonString = JsonSerializer.Serialize(objects, serializerOptions);

            File.WriteAllText(outputFilePath, jsonString);
        }

        private IEntityDescription GetOutputEntityDescription(string modelName)
        {
            var outputFileName = $"{Guid.NewGuid()}.json";

            var outputFilePath = Path.Combine(_processorOptions.OutputDirectoryPath, outputFileName);

            return new EntityDescription() { EntityType = modelName, EntityFilePath = outputFilePath };
        }

        private ParserFactory RegisterParserFactory()
        {
            var factory = new ParserFactory();

            factory.RegisterParser(FileExtension.CSV, () => new CSVParser(_csvOptions, _dynamicObjectCreateService));
            factory.RegisterParser(FileExtension.JSON, () => new JSONParser(_jsonOptions, _dynamicObjectCreateService));
            factory.RegisterParser(FileExtension.XML, () => new XMLParser(_xmlOptions, _dynamicObjectCreateService));

            return factory;
        }

        private async Task<IEnumerable<object>> ParseFile(FileInfo file, DynamicEntityWithProperties modelDescription)
        {
            var extension = file.GetFileExtension();

            var parser = GetParserByExtension(extension);

            return await parser.Parse(file, modelDescription);
        }

        private BaseParser GetParserByExtension(FileExtension fileExtension)
        {
            return _parserFactory[fileExtension];
        }
    }
}
