using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction
{
    public abstract class BaseParser
    {
        protected readonly IDynamicObjectCreateService _dynamicObjectCreateService;

        public BaseParser(IDynamicObjectCreateService dynamicObjectCreateService)
        {
            _dynamicObjectCreateService = dynamicObjectCreateService;
        }

        public abstract Task<IEnumerable<object>> Parse(FileInfo inputFile, string modelDescriptionPath);
    }
}
