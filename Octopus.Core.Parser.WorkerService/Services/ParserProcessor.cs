﻿using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Enums;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using Octopus.Core.Parser.WorkerService.Services.Factories;
using Octopus.Core.Parser.WorkerService.Services.Parsers;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services
{
    public class ParserProcessor : IParserProcessor
    {
        private readonly IQueueConsumer _consumer;
        private ParserFactory _parserFactory;

        private readonly ProcessorConfiguration _processorOptions;

        private readonly IOptions<CsvParserConfiguration> _csvOptions;
        private readonly IOptions<XmlParserConfiguration> _xmlOptions;
        private readonly IOptions<JsonParserConfiguration> _jsonOptions;
        private readonly List<string> _descriptionModelPaths;
        private readonly IDynamicObjectCreateService _dynamicObjectCreateService;

        public ParserProcessor(IQueueConsumer consumer,
            IOptions<CsvParserConfiguration> csvOptions,
            IOptions<XmlParserConfiguration> xmlOptions,
            IOptions<JsonParserConfiguration> jsonOptions,
            IOptions<ProcessorConfiguration> processorOptions,
            IDynamicObjectCreateService dynamicObjectCreateService)
        {
            _consumer = consumer;

            _processorOptions = processorOptions.Value;

            _csvOptions = csvOptions;
            _xmlOptions = xmlOptions;
            _jsonOptions = jsonOptions;

            _descriptionModelPaths = new List<string>(_processorOptions.ExpectedModelsDescriptionPaths);

            _parserFactory = RegisterParserFactory();

            _dynamicObjectCreateService = dynamicObjectCreateService;
        }

        public async Task StartProcessing(CancellationToken stoppingToken)
        {
            IEntityDescription request;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    request = await _consumer.ConsumeAsync();
                }
                catch (Exception ex)
                {
                    throw new QueueException($"{ErrorMessages.QueueException} {ex.Message}");
                }

                if (request != null)
                {
                    var modelDescriptionPath = GetExpectedModelDescriptionPath(request.EntityType);

                    var inputFile = new FileInfo(request.EntityFilePath);

                    var objects = await ParseFile(inputFile, modelDescriptionPath);

                    SerializeObjectsToOutputFile(objects);
                }

                await Task.Delay(_processorOptions.RunInterval, stoppingToken);
            }
        }

        public async Task StopProcessing(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        private string GetExpectedModelDescriptionPath(string modelName)
        {
            foreach (var path in _descriptionModelPaths)
            {
                if (path.Contains(modelName))
                {
                    return path;
                }
            }

            throw new IncorrectInputDataException(ErrorMessages.NoDescriptionProvided);
        }

        private void SerializeObjectsToOutputFile(IEnumerable<object> objects)
        {
            var outputFileName = $"{Guid.NewGuid()}.json";

            var outputFilePath = Path.Combine(_processorOptions.OutputDirectoryPath, outputFileName);

            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };

            var jsonString = JsonSerializer.Serialize(objects, serializerOptions);

            File.WriteAllText(outputFilePath, jsonString);
        }

        private ParserFactory RegisterParserFactory()
        {
            var factory = new ParserFactory();

            factory.RegisterParser(FileExtension.CSV, () => new CSVParser(_csvOptions, _dynamicObjectCreateService));
            factory.RegisterParser(FileExtension.JSON, () => new JSONParser(_jsonOptions, _dynamicObjectCreateService));
            factory.RegisterParser(FileExtension.XML, () => new XMLParser(_xmlOptions, _dynamicObjectCreateService));

            return factory;
        }

        private async Task<IEnumerable<object>> ParseFile(FileInfo file, string modelDescriptionPath)
        {
            var extension = file.GetFileExtension();

            var parser = GetParserByExtension(extension);

            return await parser.Parse(file, modelDescriptionPath);
        }

        private BaseParser GetParserByExtension(FileExtension fileExtension)
        {
            return _parserFactory[fileExtension];
        }
    }
}
