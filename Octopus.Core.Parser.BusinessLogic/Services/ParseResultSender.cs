using Octopus.Core.Common.Models;
using Octopus.Core.Parser.BusinessLogic.Interfaces.Services;
using Octopus.Core.RabbitMq.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Services
{
    public class ParseResultSender : IParseResultSender
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public ParseResultSender(IRabbitMqPublisher rabbitMqPublisher)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public async Task SendParseResult(ParserOutputData outputData)
        {
            var outputFilePath = BuildOutputFilePath(outputData.OutputDirectoryPath);

            SerializeObjectsToOutputFile(outputData.Objects, outputFilePath);

            var outputEntityDescription = new EntityDescription() 
            { 
                EntityType = outputData.ModelName, 
                EntityFilePath = outputFilePath 
            };

            await _rabbitMqPublisher.SendMessage(JsonSerializer.Serialize(outputEntityDescription));
        }

        private string BuildOutputFilePath(string directoryPath)
        {
            var outputFileName = $"{Guid.NewGuid()}.json";

            return Path.Combine(directoryPath, outputFileName);
        }

        private void SerializeObjectsToOutputFile(IEnumerable<object> objects, string outputFilePath)
        {
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };

            var jsonString = JsonSerializer.Serialize(objects, serializerOptions);

            File.WriteAllText(outputFilePath, jsonString);
        }
    }
}
