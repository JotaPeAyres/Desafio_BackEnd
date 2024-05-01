using MotorcycleRental.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Interfaces
{
    public interface IDeliveryManService
    {
        Task AddDeliveryManAsync(DeliveryMan deliveryMan, Guid userId);
        Task<IEnumerable<DeliveryMan>> GetNotifiedDeliveryMan(Guid orderId);
        Task UpdateDriverLicence(string imageName, Guid deliveryManId);
    }
}
