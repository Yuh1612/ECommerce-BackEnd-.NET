using RabbitMQ;
using RabbitMQ.Models;

namespace ECommerce.Products.RabbitMQ
{
    public class Worker : BaseWorker
    {
        public Worker(ILogger<BaseWorker> logger, RabbitMQConnection connection, IServiceProvider serviceProvider)
            : base(logger, connection, serviceProvider)
        {
        }

        public override MessageQueueSettings? Queue => RabbitMQServiceBusSettings.Queue;
    }
}