using MotorcycleRental.Api.ViewModels;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using MotorcycleRental.Bussiness.Models.Validations;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderNotificationRepository _orderNotificationRepository;
        private readonly IRabbitMqConfig _rabbitMq;
        public OrderService(INotifier notifier,
                                    IDeliveryManRepository deliveryManRepository,
                                    IOrderRepository orderRepository,
                                    IRabbitMqConfig rabbitMq,
                                    IOrderNotificationRepository orderNotificationRepository) : base(notifier)
        {
            _deliveryManRepository = deliveryManRepository;
            _orderRepository = orderRepository;
            _rabbitMq = rabbitMq;
            _orderNotificationRepository = orderNotificationRepository;
        }

        public async Task AddOrderAsync(Order order)
        {
            if (!ExecuteValidation(new OrderValidation(), order)) return;

            var deliveryMenAvailable = _deliveryManRepository.GetDeliveryManAvailable();

            if(deliveryMenAvailable.Count() == 0)
            {
                Notify("No delivery man available to create an order");
                return;   
            }

            var channel = _rabbitMq.ConnectChannel();
            
            order.CreationDate = DateTime.UtcNow;

            await _orderRepository.Add(order);

            foreach (var deliveryMan in deliveryMenAvailable)
            {
                var notification = new OrderNotificationViewModel
                {
                    DeliveryManId = deliveryMan.Id,
                    OrderId = order.Id,
                    Message = "A new order is Avaliable"
                };
                string message = JsonSerializer.Serialize(notification);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "orderQueue",
                    basicProperties: null,
                    body: body);
            }
        }

        public async Task TakeOrderAsync(Guid orderId, Guid deliveryManId)
        {
            var deliveryMan = _deliveryManRepository.GetByFirst(g => g.UserId == deliveryManId).Result;
            var order = _orderRepository.GetById(orderId).Result;

            if (deliveryMan == null)
            {
                Notify("Delivery man not found");
                return;
            }
            if (order == null)
            {
                Notify("Order not found");
                return;
            }
            if(order.Status != Order.Situations.Avaliable)
            {
                Notify("Order not found");
                return;
            }


            order.DeliveryManId = deliveryMan.Id;
            order.Status = Order.Situations.Accepted;
            await _orderRepository.Update(order);
        }

        public async Task FinalizeOrderAsync(Guid orderId, Guid deliveryManId)
        {
            var order = _orderRepository.GetById(orderId).Result;
            var deliveryMan = _deliveryManRepository.GetByFirst(g => g.UserId == deliveryManId).Result;
            if (order == null)
            {
                Notify("Order not found");
                return;
            }
            if (order.DeliveryManId != deliveryMan.Id)
            {
                Notify("Only the responsible by the order can finalize");
                return;
            }
            if (order.Status != Order.Situations.Accepted)
            {
                Notify("The order must be accepted to finalize");
                return;
            }

            order.Status = Order.Situations.Delivered;
            await _orderRepository.Update(order);
        }

        public void ReceiveNotification(string message)
        {
            var data = JsonSerializer.Deserialize<OrderNotificationViewModel>(message);

            var orderNotification = new OrderNotification
            {
                Message = data.Message,
                OrderId  = data.OrderId,
                DeliveryManId = data.DeliveryManId,
            };

            _orderNotificationRepository.Add(orderNotification);
        }
    }
}
