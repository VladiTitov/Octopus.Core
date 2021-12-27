using Microsoft.Extensions.Options;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class CSVParser : BaseParser
    {
        private readonly CsvParserConfiguration _options;

        public CSVParser(IOptions<CsvParserConfiguration> options, IDynamicObjectCreateService_2 dynamicObjectCreateService)
            : base(dynamicObjectCreateService)
        {
            _options = options.Value;
        }

        public override async Task<IEnumerable<object>> Parse(FileInfo inputFile, string modelDescriptionPath)
        {
            IEnumerable<string[]> values;
            IEnumerable<object> objects;

            try
            {
                values = await GetValues(inputFile);
            }
            catch (Exception ex)
            {
                throw new ParsingException($"{ErrorMessages.CsvParserException} {ex.Message}");
            }

            try
            {
                var extendedType = _dynamicObjectCreateService.CreateTypeByDescription(modelDescriptionPath);

                objects = _dynamicObjectCreateService.AddValuesToDynamicObject(extendedType, values);
            }
            catch (Exception ex)
            {
                throw new DynamicServiceException($"{ErrorMessages.DynamicServiceException} {ex.Message}");
            }

            return objects;
        }

        private async Task<IEnumerable<string[]>> GetValues(FileInfo inputFile)
        {
            var result = new List<string[]>();

            using (var sr = new StreamReader(inputFile.FullName, Encoding.Default))
            {
                string line;

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    result.Add(line.Split(_options.FileSeparator));
                }
            }

            return result.Skip(_options.SkipLines).ToList();
        }
    }
}
