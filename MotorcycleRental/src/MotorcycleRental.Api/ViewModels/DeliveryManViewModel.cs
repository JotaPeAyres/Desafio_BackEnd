using static MotorcycleRental.Bussiness.Models.DeliveryMan;

namespace MotorcycleRental.Api.ViewModels
{
    public class DeliveryManViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime Birthdate { get; set; }
        public int License { get; set; }
        public LicenseTypes LicenseType { get; set; }
        public string LicenseImage { get; set; }
    }
}
