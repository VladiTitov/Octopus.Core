using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Octopus.Core.Parser.BusinessLogic.Interfaces.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.BusinessLogic.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IMongoRepository _mongoRepository;

        public ValidationService(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<ParserInputData> ValidateEntityDescription(IEntityDescription request)
        {
            if (request == null) throw new IncorrectInputDataException(ErrorMessages.EmptyRequest);

            var dynamicEntity = await ValidateInputDynamicEntity(request.EntityType);

            var inputFile = ValidateInputFile(request.EntityFilePath);

            return new ParserInputData { DynamicEntity = dynamicEntity, InputFile = inputFile };
        }

        private async Task<DynamicEntityWithProperties> ValidateInputDynamicEntity(string modelName)
        {
            DynamicEntityWithProperties dynamicEntity;

            try
            {
                dynamicEntity = await _mongoRepository.GetEntity(modelName);
            }
            catch (Exception)
            {
                throw;
            }

            if (dynamicEntity == null) throw new IncorrectInputDataException(ErrorMessages.NoDescriptionProvided);

            return dynamicEntity;
        }

        private FileInfo ValidateInputFile(string filename)
        {
            if (!File.Exists(filename)) throw new IncorrectInputDataException(ErrorMessages.InputFileNotExist);

            var inputFile = new FileInfo(filename);

            if (!inputFile.IsAvailable()) throw new IncorrectInputDataException(ErrorMessages.InputFileNotAvailable);

            return inputFile;
        }
    }
}
