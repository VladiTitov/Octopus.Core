using Octopus.Core.Common.Enums;
using Octopus.Core.Parser.WorkerService.Services.Parsers.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopus.Core.Parser.WorkerService.Services.Factories
{
    public class ParserFactory
    {
        private readonly Dictionary<FileExtension, Func<BaseParser>> _parsers;

        public ParserFactory()
        {
            _parsers = new Dictionary<FileExtension, Func<BaseParser>>();
        }

        public BaseParser this[FileExtension extension] => CreateParser(extension);

        public BaseParser CreateParser(FileExtension extension) => _parsers[extension]();

        public FileExtension[] RegisteredTypes => _parsers.Keys.ToArray();

        public void RegisterParser(FileExtension extension, Func<BaseParser> factoryMethod)
        {
            if (factoryMethod is null)
            {
                return;
            }

            _parsers[extension] = factoryMethod;
        }
    }
}
