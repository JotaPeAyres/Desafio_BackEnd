using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Bussiness.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorcycleRental.Api.RabbitMq
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public RabbitMqConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var channel = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IRabbitMqConfig>().ConnectChannel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (moduleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IOrderService>().ReceiveNotification(message);
            };

            channel.BasicConsume(queue: "orderQueue", autoAck: true, consumer: consumer);

            return Task.Delay(Timeout.Infinite);
        }
    }
}
