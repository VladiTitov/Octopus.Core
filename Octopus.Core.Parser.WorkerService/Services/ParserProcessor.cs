using Microsoft.Extensions.Options;
using Octopus.Core.Common.Enums;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services;
using Octopus.Core.Parser.WorkerService.Services.Factories;
using Octopus.Core.Parser.WorkerService.Services.Parsers;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.IO;
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

        public ParserProcessor(IQueueConsumer consumer,
            IOptions<CsvParserConfiguration> csvOptions,
            IOptions<XmlParserConfiguration> xmlOptions,
            IOptions<JsonParserConfiguration> jsonOptions,
            IOptions<ProcessorConfiguration> processorOptions)
        {
            _consumer = consumer;

            _processorOptions = processorOptions.Value;

            _csvOptions = csvOptions;
            _xmlOptions = xmlOptions;
            _jsonOptions = jsonOptions;

            _parserFactory = RegisterParserFactory();
        }

        public async Task StartProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var request = await _consumer.ConsumeAsync();

                if (request != null)
                {
                    var inputFile = new FileInfo(request.EntityFilePath);

                    ParseFile(inputFile);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        public async Task StopProcessing(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        private ParserFactory RegisterParserFactory()
        {
            var factory = new ParserFactory();

            factory.RegisterParser(FileExtension.CSV, () => new CSVParser(_csvOptions));
            factory.RegisterParser(FileExtension.JSON, () => new JSONParser(_jsonOptions));
            factory.RegisterParser(FileExtension.XML, () => new XMLParser(_xmlOptions));

            return factory;
        }

        private void ParseFile(FileInfo file)
        {
            var extension = file.GetFileExtension();

            var parser = GetParserByExtension(extension);

            parser.Parse(file);
        }

        private BaseParser GetParserByExtension(FileExtension fileExtension)
        {
            return _parserFactory[fileExtension];
        }
    }
}
