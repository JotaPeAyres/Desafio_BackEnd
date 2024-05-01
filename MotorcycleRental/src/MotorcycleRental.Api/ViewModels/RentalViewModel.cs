using static MotorcycleRental.Bussiness.Models.Rental;

namespace MotorcycleRental.Api.ViewModels
{
    public class RentalViewModel
    {
        public RentalPlans RentalPlan { get; set; }
        public Guid DeliveryManId { get; set; }
        public Guid MotorcycleId { get; set; }
    }
}
