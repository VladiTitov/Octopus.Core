using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction
{
    public abstract class BaseParser
    {
        protected readonly IDynamicObjectCreateService_2 _dynamicObjectCreateService;

        public BaseParser(IDynamicObjectCreateService_2 dynamicObjectCreateService)
        {
            _dynamicObjectCreateService = dynamicObjectCreateService;
        }

        public abstract Task<IEnumerable<object>> Parse(FileInfo inputFile, string modelDescriptionPath);
    }
}
