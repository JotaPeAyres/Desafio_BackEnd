using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models
{
    public class DeliveryMan : Entity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime Birthdate { get; set; }
        public int LicenseNumber { get; set; }
        public LicenseTypes LicenseType { get; set; }
        public string LicenseImage { get; set; }


        public enum LicenseTypes 
        {
            A,
            B,
            AB
        }

    }
}
