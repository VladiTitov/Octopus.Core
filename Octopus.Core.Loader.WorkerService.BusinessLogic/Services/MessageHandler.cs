﻿using System;
using Octopus.Core.Common.Models;
using Octopus.Core.Loader.BusinessLogic.Interfaces;
using Octopus.Core.RabbitMq.Services.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Octopus.Core.Loader.BusinessLogic.Services
{
    public class MessageHandler : IEventProcessor
    {
        private readonly ILogger<MessageHandler> _logger;
        private readonly IDynamicEntityService _dynamicEntityService;
        private readonly IDataReaderService _dataReaderService;

        public MessageHandler(ILogger<MessageHandler> logger, IDataReaderService dataReaderService,
            IDynamicEntityService dynamicEntityService)
        {
            _logger = logger;
            _dataReaderService = dataReaderService;
            _dynamicEntityService = dynamicEntityService;
        }

        public async void ProcessEvent(string message)
        {
            var entityDescription = GetEntityDescription(message);
            if (entityDescription == null) return;
            var objects = await _dataReaderService.GetDataFromFileAsync(entityDescription.EntityFilePath);
            await _dynamicEntityService.AddRangeAsync(objects);
        }

        public IEntityDescription GetEntityDescription(string message)
        {
            try
            {
                return JsonSerializer.Deserialize<IEntityDescription>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
