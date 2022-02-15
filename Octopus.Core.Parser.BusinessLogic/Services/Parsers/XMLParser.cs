using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Octopus.Core.Common.ConfigsModels.Parsers;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Parser.BusinessLogic.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Octopus.Core.Parser.BusinessLogic.Services.Parsers
{
    public class XMLParser : BaseParser
    {
        private readonly XmlParserConfiguration _options;

        public XMLParser(IOptions<XmlParserConfiguration> options,
            IDynamicObjectCreateService dynamicObjectCreateService)
            : base(dynamicObjectCreateService)
        {
            _options = options.Value;
        }

        public async override Task<IEnumerable<object>> Parse(FileInfo inputFile,
            DynamicEntityWithProperties modelDescription)
        {
            Type extendedType;

            try
            {
                extendedType = _dynamicObjectCreateService.CreateTypeByDescription(modelDescription);
            }
            catch (Exception ex)
            {
                throw new DynamicServiceException($"{ErrorMessages.DynamicServiceException} {ex.Message}");
            }

            try
            {
                return GetObjects(extendedType, inputFile.FullName);
            }
            catch (Exception ex)
            {
                throw new ParsingException($"{ErrorMessages.XmlParserException} {ex.Message}");
            }
        }

        private IEnumerable<object> GetObjects(Type extendedType, string fileName)
        {
            var xmldoc = new XmlDocument();
            xmldoc.Load(fileName);
            var rootElement = xmldoc.DocumentElement;

            var parsedObjects = new List<dynamic>();

            foreach (XmlNode item in rootElement.ChildNodes)
            {
                var json = JsonConvert.SerializeXmlNode(item, Newtonsoft.Json.Formatting.None, true);
                var dynamicEntity = JsonConvert.DeserializeObject(json, extendedType);

                parsedObjects.Add(dynamicEntity);
            }

            return parsedObjects;
        }
    }
}
