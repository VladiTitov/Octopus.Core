using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Core.Common.ConfigsModels.ConnectionStrings;
using Octopus.Core.Common.ConfigsModels.Rabbit;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;
using Octopus.Core.Common.DynamicObject.Services;
using Octopus.Core.Common.DynamicObject.Services.Interfaces;
using Octopus.Core.Common.Helpers.JsonDeserializer;
using Octopus.Core.Common.Models;
using Octopus.Core.Loader.BusinessLogic.Interfaces;
using Octopus.Core.Loader.BusinessLogic.Services;
using Octopus.Core.Loader.DataAccess.DatabaseContext;
using Octopus.Core.Loader.DataAccess.Interfaces;
using Octopus.Core.Loader.DataAccess.Repositories;
using Octopus.Core.Loader.DataAccess.Services;
using Octopus.Core.RabbitMq.Context;
using Octopus.Core.RabbitMq.Services;
using Octopus.Core.RabbitMq.Services.Implementations;
using Octopus.Core.RabbitMq.Services.Interfaces;

namespace Octopus.Core.Loader.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(confBuilder =>
                {
                    confBuilder.AddJsonFile("appsettings.json");
                    confBuilder.AddJsonFile("Configs/rabbitMq-config.json");
                    confBuilder.AddJsonFile("Configs/connectionString-config.json");
                    confBuilder.AddJsonFile("Configs/rabbitMqConnectionString-config.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitParams"));
                    services.Configure<ConnectionStringConfig>(hostContext.Configuration.GetSection("ConnectionString"));
                    services.Configure<ConnectionConfiguration>(hostContext.Configuration.GetSection("RabbitMqConnectionString"));

                    services.AddSingleton(hostContext.Configuration.GetSection("Subscribers").Get<IEnumerable<SubscriberConfiguration>>());
                    services.AddSingleton(hostContext.Configuration.GetSection("Publishers").Get<IEnumerable<PublisherConfiguration>>());

                    services.AddSingleton<IDataReaderService, DataReaderService>();
                    services.AddSingleton<IEntityDescription, EntityDescription>();
                    services.AddSingleton<IDynamicObjectCreateService, DynamicObjectCreateService>();
                    services.AddSingleton<IDynamicTypeFactory, DynamicTypeFactory>();
                    services.AddSingleton<IDynamicEntityService, DynamicEntityService>();
                    services.AddSingleton<IDynamicEntityRepository, DynamicEntityRepository>();

                    services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
                    services.AddSingleton<IDataReaderService, DataReaderService>();

                    services.AddSingleton<IDatabaseProvidersFactory, DatabaseProvidersFactory>();
                    services.AddSingleton<IMigrationCreateService, MigrationCreateService>();
                    services.AddSingleton<IQueryFactoryService, QueryFactoryService>();
                    services.AddSingleton<IQueryHandlerService, QueryHandlerService>();

                    services.AddSingleton<IDatabaseContext, DapperDbContext>();

                    #region RabbitMQ

                    services.AddSingleton<IRabbitMqContext, RabbitMqContext>();

                    services.AddSingleton<IRabbitMqInitializer, RabbitMqInitializer>();
                    services.AddSingleton<IRabbitMqSubscriber, RabbitMqSubscriber>();
                    services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
                    services.AddSingleton<IEventProcessor, MessageHandler>();

                    #endregion

                    services.AddHostedService<LoaderService>();
                });
        
    }
}
