using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models;
using MotorcycleRental.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Data.Repositories
{
    public class DeliveryManRepository : Repository<DeliveryMan>, IDeliveryManRepository
    {
        public DeliveryManRepository(ApiDbContext dbContext) : base(dbContext) { }

        public IEnumerable<DeliveryMan> GetDeliveryManAvailable()
        {
            var deliveryMenWithRentIds = Db.Rentals.Where(w => !w.IsFinished).Select(s => s.DeliveryManId);

            var deliveryMen = Db.DeliveryMen.Where(w => deliveryMenWithRentIds.Contains(w.Id));

            var deliveryManWithOrder = Db.Orders.Include(i => i.DeliveryMan)
                .Where(w => w.DeliveryManId != null).Select(s => s.DeliveryManId);

            return deliveryMen.Where(w => !deliveryManWithOrder.Contains(w.Id));
        }

        public IEnumerable<DeliveryMan> GetNotifiedDeliveryMan(Guid orderId)
        {
            var deliveryManId = Db.OrderNotifications.Where(w => w.OrderId == orderId).Select(s => s.DeliveryManId);

            return Db.DeliveryMen.Where(w => deliveryManId.Contains(w.Id));
        }
    }
}
