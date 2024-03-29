﻿using System.Collections.Generic;
using Octopus.Core.Common.ConfigsModels.Rabbit.Base;

namespace Octopus.Core.RabbitMq.Interfaces
{
    public interface IRabbitMqSubscriber
    {
        void StartService(IEnumerable<SubscriberConfiguration> configurations);
        void StopService();
    }
}
