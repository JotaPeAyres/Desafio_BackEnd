using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(f => f.Value)
                .NotEmpty().WithMessage("{PropertyName} field is required");

            //RuleFor(f => f.Status)
            //    .NotEmpty().WithMessage("{PropertyName} field is required");

            //RuleFor(f => f.CreationDate)
            //    .NotEmpty().WithMessage("{PropertyName} field is required");

        }
    }
}
