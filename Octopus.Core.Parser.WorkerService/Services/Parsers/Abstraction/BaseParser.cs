using System.Collections.Generic;
using System.IO;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction
{
    public abstract class BaseParser
    {
        public abstract IEnumerable<object> Parse(FileInfo inputFile);
    }
}
