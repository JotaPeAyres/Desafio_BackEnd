using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models
{
    public class Rental : Entity
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedFinalDate { get; set; }
        public decimal TotalCust { get; set; }
        public RentalPlans RentalPlan { get; set; }
        public Guid DeliveryManId { get; set; }
        public Guid MotorcycleId { get; set; }
        public bool IsFinished { get; set; }

        public enum RentalPlans 
        {
            [Description("7 days")]
            shortPlan = 7,
            [Description("15 days")]
            mediumPlan  = 15,
            [Description("30 days")]
            LargePlan = 30,
        }



        /* Relations */
        public virtual DeliveryMan DeliveryMan { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }
    }
}
