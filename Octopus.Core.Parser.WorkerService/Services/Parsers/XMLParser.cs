﻿using Microsoft.Extensions.Options;
using Octopus.Core.Parser.WorkerService.Configuration.Implementations;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;

namespace Octopus.Core.Parser.WorkerService.Services.Parsers
{
    public class XMLParser : BaseParser
    {
        private readonly XmlParserConfiguration _options;

        public XMLParser(IOptions<XmlParserConfiguration> options)
        {
            _options = options.Value;
        }

        public override IEnumerable<string> Parse(FileInfo inputFile)
        {
            throw new NotImplementedException();
        }
    }
}
