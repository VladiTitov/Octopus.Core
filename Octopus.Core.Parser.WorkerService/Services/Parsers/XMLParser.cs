using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Octopus.Core.Common.Models;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Interfaces.Services.DynamicModels;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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
            var extendedType = _dynamicObjectCreateService.CreateTypeByDescription(modelDescriptionPath);

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
    }
}
