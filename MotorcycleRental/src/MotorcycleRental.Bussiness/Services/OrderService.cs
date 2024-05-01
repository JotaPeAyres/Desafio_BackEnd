using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using MotorcycleRental.Bussiness.Models.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        public OrderService(INotifier notifier,
                                    IDeliveryManRepository deliveryManRepository) : base(notifier)
        {
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task AddOrderAsync(Order order)
        {
            if (!ExecuteValidation(new OrderValidation(), order)) return;

            var DeliveryMenAvailable = _deliveryManRepository.GetDeliveryManAvailable();


        }
    }
}
