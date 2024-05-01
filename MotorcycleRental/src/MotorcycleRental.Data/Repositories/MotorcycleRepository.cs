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
    public class MotorcycleRepository : Repository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(ApiDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsRented(Guid motorcycleId)
        {
            return await Db.Rentals.AsNoTracking()
                .AnyAsync(w => w.MotorcycleId == motorcycleId);
        }
    }
}
