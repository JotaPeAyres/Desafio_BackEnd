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
    public class OrderNotificationRepository : Repository<OrderNotification>, IOrderNotificationRepository
    {
        public OrderNotificationRepository(ApiDbContext dbContext) : base(dbContext) { }
    }
}
