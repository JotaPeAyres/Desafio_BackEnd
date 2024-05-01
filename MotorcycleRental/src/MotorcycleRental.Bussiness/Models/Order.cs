using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models
{
    public class Order : Entity
    {
        public DateTime CreationDate { get; set; }
        public decimal Value { get; set; }
        public Situations Status { get; set; }

        [ForeignKey("DeliveryMan")]
        public Guid? DeliveryManId { get; set; }
        public virtual DeliveryMan DeliveryMan { get; set; }

        public enum Situations 
        { 
            Avaliable,
            Accepted,
            Delivered
        }

    }
}
