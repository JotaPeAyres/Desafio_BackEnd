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
            var deliveryMenWithRentIds = Db.Rentals.Where(w => !w.IsFinished)
                .Select(s => s.Id);

            return Db.Orders
                .Include(i => i.DeliveryMan)
                .Where(w => w.DeliveryManId != null && !deliveryMenWithRentIds.Contains((Guid)w.DeliveryManId)).Select(s => s.DeliveryMan);
        }
    }
}
