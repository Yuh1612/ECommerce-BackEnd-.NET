using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;
using RabbitMQ.Models;
using System.Text;
using System.Text.Json;

namespace RabbitMQ
{
    public abstract class BaseWorker : BackgroundService
    {
        protected readonly IServiceProvider ServiceProvider;

        protected readonly RabbitMQConnection Connection;

        protected readonly ILogger<BaseWorker> Logger;

        public abstract MessageQueueSettings? Queue { get; }

        public BaseWorker(ILogger<BaseWorker> logger, RabbitMQConnection connection, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Connection = connection;
            Logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queue = Connection.Channel.QueueDeclare(queue: Queue?.QueueUrl,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Connection.Channel.QueueBind(queue: queue,
                              exchange: RabbitMQServiceBusSettings.Exchange,
                              routingKey: "");

            Connection.Channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(Connection.Channel);
            consumer.Received += Consumer_Received;

            Connection.Channel.BasicConsume(queue: Queue?.QueueUrl,
                                 autoAck: false,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            using var scope = ServiceProvider.CreateScope();

            string body = Encoding.UTF8.GetString(e.Body.ToArray());

            var messageQueue = JsonSerializer.Deserialize<MessageQueue>(body);
            messageQueue!.QueueName = this.Queue?.QueueUrl;

            var message = MessageQueueConsumption.Parse(messageQueue, e.RoutingKey);

            Logger.LogInformation($"Start consume event: {message.EventName}, Body: {message}");

            var consumptionService = scope.ServiceProvider.GetRequiredService<IEventConsumptionService>();
            await consumptionService.ProcessAsync(message);

            Connection.Channel.BasicAck(e.DeliveryTag, multiple: false);
        }
    }
}