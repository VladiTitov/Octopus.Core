using Octopus.Core.Common.DynamicObject.Models;
using System.IO;

namespace Octopus.Core.Common.Models
{
    public class ParserInputData
    {
        public FileInfo InputFile { get; set; }

        public DynamicEntityWithProperties DynamicEntity { get; set; }
    }
}
