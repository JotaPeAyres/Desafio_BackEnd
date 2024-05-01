using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models
{
    public class Motorcycle : Entity
    {
        public string Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
    }
}
