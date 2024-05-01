using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models
{
    public class OrderNotification : Entity
    {
        public Guid OrderId { get; set; }
        public Guid DeliveryManId { get; set; }
        public string Message { get; set; }
    }
}
