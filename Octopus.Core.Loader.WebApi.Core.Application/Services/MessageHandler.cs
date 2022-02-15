using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Common.Exceptions;
using Octopus.Core.Common.Extensions;
using Octopus.Core.Common.Models;
using Octopus.Core.RabbitMq.Interfaces;
using Octopus.Core.Loader.WebApi.Core.Application.Interfaces;

namespace Octopus.Core.Loader.WebApi.Core.Application.Services
{
    public class MessageHandler : IEventProcessor
    {
        private readonly ILogger<MessageHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MessageHandler(ILogger<MessageHandler> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async void ProcessEvent(string message)
        {
            try
            {
                await LoadDataInDatabase(message.GetEntityDescription());
            }
            catch (ParsingException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (MongoDbException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (DatabaseProviderException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task LoadDataInDatabase(IEntityDescription entityDescription)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dataReaderService = scope.ServiceProvider.GetRequiredService<IDataReaderService>();
                var objects = await dataReaderService.GetDataFromFileAsync(entityDescription);
                var dynamicEntityService = scope.ServiceProvider.GetRequiredService<IDynamicEntityService>();
                await dynamicEntityService.AddRangeAsync(objects);
            }
        }
    }
}
