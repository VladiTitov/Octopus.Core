using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction
{
    public abstract class BaseParser
    {
        public abstract Task<IEnumerable<string[]>> Parse(FileInfo inputFile);
    }
}
