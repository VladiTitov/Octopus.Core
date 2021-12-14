using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Octopus.Core.Loader.RabbitMq.Services
{
    public interface IRabbitMqListener
    {
        void ChannelConsume();
        EventingBasicConsumer GetConsumer();
    }
}
