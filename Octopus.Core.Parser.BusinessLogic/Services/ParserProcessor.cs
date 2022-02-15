using Microsoft.Extensions.Options;
using Octopus.Core.Common.ConfigsModels.Parsers;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Enums;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.Parser.BusinessLogic.Interfaces.Services;
using Octopus.Core.Parser.BusinessLogic.Services.Factories;
using Octopus.Core.Parser.BusinessLogic.Services.Parsers;
using Octopus.Core.Parser.BusinessLogic.Services.Parsers.Abstraction;
using System.Collections.Generic;
using System.IO;
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
        private readonly IParseResultSender _parseResultSender;

        public ParserProcessor(IOptions<CsvParserConfiguration> csvOptions,
            IOptions<XmlParserConfiguration> xmlOptions,
            IOptions<JsonParserConfiguration> jsonOptions,
            IOptions<ProcessorConfiguration> processorOptions,
            IDynamicObjectCreateService dynamicObjectCreateService,
            IParseResultSender parseResultSender)
        {
            _processorOptions = processorOptions.Value;

            _csvOptions = csvOptions;
            _xmlOptions = xmlOptions;
            _jsonOptions = jsonOptions;

            _parserFactory = RegisterParserFactory();

            _dynamicObjectCreateService = dynamicObjectCreateService;
            _parseResultSender = parseResultSender;
        }

        public async Task ProcessInputData(ParserInputData inputData)
        {
            var objects = await Parse(inputData.InputFile, inputData.DynamicEntity);

            var parserOutputData = new ParserOutputData 
            { 
                ModelName = inputData.DynamicEntity.EntityName,
                Objects = objects,
                OutputDirectoryPath = _processorOptions.OutputDirectoryPath
            };

            await _parseResultSender.SendParseResult(parserOutputData);
        }

        public Task StartProcessing()
        {
            return Task.CompletedTask;
        }

        public Task StopProcessing()
        {
            return Task.CompletedTask;
        }

        private async Task<IEnumerable<object>> Parse(FileInfo inputFile, DynamicEntityWithProperties model)
        {
            var fileExtension = inputFile.GetFileExtension();

            var parser = GetParserByExtension(fileExtension);

            return await parser.Parse(inputFile, model);
        }

        private ParserFactory RegisterParserFactory()
        {
            var factory = new ParserFactory();

            factory.RegisterParser(FileExtension.CSV, () => new CSVParser(_csvOptions, _dynamicObjectCreateService));
            factory.RegisterParser(FileExtension.JSON, () => new JSONParser(_jsonOptions, _dynamicObjectCreateService));
            factory.RegisterParser(FileExtension.XML, () => new XMLParser(_xmlOptions, _dynamicObjectCreateService));

            return factory;
        }

        private BaseParser GetParserByExtension(FileExtension fileExtension)
        {
            return _parserFactory[fileExtension];
        }
    }
}