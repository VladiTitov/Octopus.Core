using Microsoft.Extensions.Options;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class JSONParser : BaseParser
    {
        private readonly JsonParserConfiguration _options;

        public JSONParser(IOptions<JsonParserConfiguration> options, IDynamicObjectCreateService dynamicObjectCreateService)
            : base(dynamicObjectCreateService)
        {
            _options = options.Value;
        }

        public override Task<IEnumerable<object>> Parse(FileInfo inputFile, string modelDescriptionPath)
        {
            throw new NotImplementedException();
        }
    }
}
