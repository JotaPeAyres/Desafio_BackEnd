using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using MotorcycleRental.Bussiness.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Services
{
    public class DeliveryManService : BaseService, IDeliveryManService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        public DeliveryManService (INotifier notifier,
                                    IDeliveryManRepository deliveryManRepository) : base (notifier) 
        {
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task AddDeliveryManAsync(DeliveryMan deliveryMan, Guid userId)
        {
            if(!ExecuteValidation(new DeliveryManValidation(), deliveryMan)) return;

            if(_deliveryManRepository.GetBy(g => g.Document == deliveryMan.Document).Result.Any())
            {
                Notify("There is already a delivery man with this CNPJ");
                return;
            }

            if (_deliveryManRepository.GetBy(g => g.LicenseNumber == deliveryMan.LicenseNumber).Result.Any())
            {
                Notify("There is already a delivery man with this driver's license");
                return;
            }

            deliveryMan.UserId = userId;

            await _deliveryManRepository.Add(deliveryMan);
        }
    }
}
