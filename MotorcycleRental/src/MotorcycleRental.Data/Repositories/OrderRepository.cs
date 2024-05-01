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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApiDbContext dbContext) : base(dbContext) { }

    }
}
