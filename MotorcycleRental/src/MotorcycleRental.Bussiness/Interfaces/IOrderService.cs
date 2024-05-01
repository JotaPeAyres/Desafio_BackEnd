using MotorcycleRental.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(Order order);
    }
}
