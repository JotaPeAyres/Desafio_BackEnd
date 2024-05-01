using MotorcycleRental.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Interfaces
{
    public interface IRentalService
    {
        Task AddRentalAsync(Rental rental);
        Task<decimal> FinishRentalAsync(Guid rentalId, DateTime endDate);
    }
}
