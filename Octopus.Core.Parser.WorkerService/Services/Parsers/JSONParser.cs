using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class JSONParser : BaseParser
    {
        public override IEnumerable<object> Parse(FileInfo inputFile)
        {
            throw new NotImplementedException();
        }
    }
}
