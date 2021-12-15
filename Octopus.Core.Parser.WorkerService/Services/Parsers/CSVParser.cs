using Microsoft.Extensions.Options;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class CSVParser : BaseParser
    {
        private readonly CsvParserConfiguration _options;

        public CSVParser(IOptions<CsvParserConfiguration> options)
        {
            _options = options.Value;
        }

        public override IEnumerable<string> Parse(FileInfo inputFile)
        {
            throw new NotImplementedException();
        }
    }
}
