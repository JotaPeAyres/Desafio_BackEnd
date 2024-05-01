using FluentValidation.Validators;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.RabbitMq
{
    public class RabbiMqConfig
    {
        public IModel ConnectChannel()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            {
                channel.QueueDeclare(
                    queue: "orderQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            }
            return channel;
        }
    }
}
