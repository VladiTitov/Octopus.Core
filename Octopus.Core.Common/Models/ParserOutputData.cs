using System.Collections.Generic;

namespace Octopus.Core.Common.Models
{
    public class ParserOutputData
    {
        public string ModelName { get; set; }

        public IEnumerable<object> Objects { get; set; }

        public string OutputDirectoryPath { get; set; }
    }
}
