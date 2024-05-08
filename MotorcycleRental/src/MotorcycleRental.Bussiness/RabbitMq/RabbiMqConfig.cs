using FluentValidation.Validators;
using Microsoft.Extensions.Configuration;
using MotorcycleRental.Bussiness.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.RabbitMq
{
    public class RabbiMqConfig : IRabbitMqConfig
    {
        private readonly IConfiguration _configuration;

        public RabbiMqConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IModel ConnectChannel()
        {
            var factory = new ConnectionFactory() { HostName = _configuration["rabbitMQConfig"], Port = 5672 };
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
