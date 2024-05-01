using Microsoft.Extensions.DependencyInjection;
using MotorcycleRental.Bussiness.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorcycleRental.Api.RabbitMq
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IRabbitMqConfig _rabbitMqConfig;    
        private readonly IOrderService _orderService;
        public RabbitMqConsumer(IRabbitMqConfig rabbitMqConfig, IOrderService orderService)
        {
            _rabbitMqConfig = rabbitMqConfig;
            _orderService = orderService;

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var channel = _rabbitMqConfig.ConnectChannel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (moduleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _orderService.ReceiveNotification(message);
            };

            channel.BasicConsume(queue: "orderQueue", autoAck: true, consumer: consumer);

            return Task.Delay(Timeout.Infinite);
        }
    }
}
