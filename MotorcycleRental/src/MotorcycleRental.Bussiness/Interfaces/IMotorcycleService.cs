using MotorcycleRental.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Interfaces
{
    public interface IMotorcycleService
    {
        Task AddMotorcycleAsync(Motorcycle motorcycle);
        Task UpdateMotorcycleAsync(Motorcycle motorcycle);
        Task DeleteMotorcycleAsync(Guid id);
    }
}
