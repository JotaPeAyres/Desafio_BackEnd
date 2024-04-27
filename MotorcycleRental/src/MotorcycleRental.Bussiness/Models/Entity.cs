using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models
{
    public abstract class Entity
    {
        //protected Entity() 
        //{
        //    Id = new Guid.NewGuid();
        //}

        public Guid Id { get; set; }
    }
}
