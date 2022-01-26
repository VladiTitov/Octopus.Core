using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class XMLParser : BaseParser
    {
        private readonly XmlParserConfiguration _options;

        public XMLParser(IOptions<XmlParserConfiguration> options, IDynamicObjectCreateService dynamicObjectCreateService)
            : base(dynamicObjectCreateService)
        {
            _options = options.Value;
        }

        public async override Task<IEnumerable<object>> Parse(FileInfo inputFile, string modelDescriptionPath)
        {
            Type extendedType;

            try
            {
                extendedType = _dynamicObjectCreateService.CreateTypeByDescription(modelDescriptionPath);
            }
            catch (Exception ex)
            {
                throw new DynamicServiceException($"{ErrorMessages.DynamicServiceException} {ex.Message}");
            }
            
            try
            {
                var xmldoc = new XmlDocument();
                xmldoc.Load(inputFile.FullName);

                var rootElement = xmldoc.DocumentElement;
                var list = new List<dynamic>();

                foreach (XmlNode item in rootElement.ChildNodes)
                {
                    var json = JsonConvert.SerializeXmlNode(item, Newtonsoft.Json.Formatting.None, true);
                    var dynamicEntity = JsonConvert.DeserializeObject(json, extendedType);

                    list.Add(dynamicEntity);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new ParsingException($"{ErrorMessages.XmlParserException} {ex.Message}");
            }
        }
    }
}
