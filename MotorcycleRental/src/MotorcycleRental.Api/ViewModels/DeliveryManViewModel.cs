using System.Text.Json.Serialization;
using static MotorcycleRental.Bussiness.Models.DeliveryMan;

namespace MotorcycleRental.Api.ViewModels
{
    public class DeliveryManViewModel
    {
        [JsonIgnore]    
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime Birthdate { get; set; }
        public int LicenseNumber { get; set; }
        public LicenseTypes LicenseType { get; set; }
        [JsonIgnore]    
        public string? LicenseImage { get; set; }
    }
}
