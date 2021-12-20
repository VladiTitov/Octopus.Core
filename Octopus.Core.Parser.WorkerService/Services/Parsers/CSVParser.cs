using Microsoft.Extensions.Options;
using Octopus.Core.Parser.WorkerService.Configs.Implementations;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
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

        public CSVParser(IOptions<CsvParserConfiguration> options)
        {
            _options = options.Value;
        }

        public override async Task<IEnumerable<string[]>> Parse(FileInfo inputFile)
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
