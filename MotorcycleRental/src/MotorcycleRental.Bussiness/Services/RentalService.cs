using MotorcycleRental.Bussiness.Interfaces;
using MotorcycleRental.Bussiness.Models.Validations;
using MotorcycleRental.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static MotorcycleRental.Bussiness.Models.Rental;

namespace MotorcycleRental.Bussiness.Services
{
    public class RentalService : BaseService, IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        public RentalService(IRentalRepository rentalRepository,
                            IDeliveryManRepository deliveryManRepository,
                            IMotorcycleRepository motorcycleRepository,
                            INotifier notifier) : base(notifier)
        {
            _rentalRepository = rentalRepository;   
            _deliveryManRepository = deliveryManRepository;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task AddRentalAsync(Rental rental)
        {
            //if (!ExecuteValidation(new RentalValidation(), rental)) return false;

            var deliveryMan = await _deliveryManRepository.GetById(rental.DeliveryManId);
            if (deliveryMan.LicenseType == DeliveryMan.LicenseTypes.B)
            {
                Notify("Only delivery drivers with a category A or AB driver's license can rent");
                return;
            }

            if (_motorcycleRepository.IsRented(rental.MotorcycleId).Result)
            {
                Notify("This motorcycle is on a rental");
                return;
            }

            var totalCust = RentalTotalCust(rental.RentalPlan);
            if(totalCust <= 0)
            {
                Notify("This rental plan is not valid");
                return;
            }

            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(1 + (int)rental.RentalPlan);

            rental.EndDate = endDate;
            rental.BeginDate = startDate;
            rental.ExpectedFinalDate = endDate;
            rental.TotalCust = totalCust;

            await _rentalRepository.Add(rental);
        }

        public async Task<decimal> FinishRentalAsync(Guid rentalId, DateTime endDate)
        {
            var rental = await _rentalRepository.GetById(rentalId);
            if (rental == null)
            {
                Notify("Rental not found!");
                return 0;
            }

            var finalCust = 0.0m;
            if(rental.ExpectedFinalDate > endDate)
            {
                var daysRented = DateTime.Now.Date - rental.BeginDate;
                var custRented  = daysRented.Days * RentalValues(rental.RentalPlan); 

                var daysLeft = DateTime.Now.Date - endDate.Date;
                var fineCust = daysLeft.Days * PenaltyChargePercentage(rental.RentalPlan);

                finalCust = custRented + fineCust;
                rental.TotalCust = finalCust;
            }
            else if (rental.ExpectedFinalDate < endDate)
            {
                var custRented = RentalTotalCust(rental.RentalPlan);

                var extraDays = endDate.Date - rental.ExpectedFinalDate;
                var fineCust = extraDays.Days * 50.00m;

                finalCust = custRented + finalCust;
                rental.TotalCust = finalCust;
            }
            else
            {
                finalCust = rental.TotalCust;
            }
            rental.IsFinished = true;
            await _rentalRepository.Update(rental);

            return finalCust;
        }

        private int RentalValues(RentalPlans rentalPlan)
        {
            switch (rentalPlan)
            {
                case RentalPlans.shortPlan:
                    return 30;
                case RentalPlans.mediumPlan:
                    return 28;
                case RentalPlans.LargePlan:
                    return 22;
                default:
                    return 0;
            }
        }

        private  decimal RentalTotalCust(RentalPlans rentalPlan)
        {
            switch (rentalPlan) 
            {
                case RentalPlans.shortPlan:
                    return 7 * 30.00m;
                case RentalPlans.mediumPlan:
                    return 15 * 28.00m;
                case RentalPlans.LargePlan:
                    return 30 * 22.00m;
                default:
                    return 0m;
            }
        }

        private  decimal PenaltyChargePercentage(RentalPlans rentalPlan)
        {
            switch (rentalPlan)
            {
                case RentalPlans.shortPlan:
                    return 0.2m;
                case RentalPlans.mediumPlan:
                    return 0.4m;
                case RentalPlans.LargePlan:
                    return 0.6m;
                default:
                    return 0m;
            }
        }
    }
}
