using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorcycleRental.Bussiness.Models.Validations;

namespace MotorcycleRental.Bussiness.Models.Validations
{
    public class MotorcycleValidation : AbstractValidator<Motorcycle>
    {
        public MotorcycleValidation()
        {
            RuleFor(f => f.LicensePlate.Length).Equal(7)
                .WithMessage("the size of the license plate must be {ComparisonValue}."); ;
            RuleFor(f => LicensePlateValidation.ValidatePlate(f.LicensePlate)).Equal(true)
                .WithMessage("The license plate provided is invalid");

            RuleFor(f => f.Year)
                .NotEmpty().WithMessage("{PropertyName} field is required")
                .Length(4, 4).WithMessage("{PropertyName} field must have {MaxLength} digits");

            RuleFor(f => f.Model)
                .NotEmpty().WithMessage("{PropertyName} field is required")
                .Length(2, 200).WithMessage("The size of the {PropertyName} field must be between {MinLength} and {MaxLength}");
        }
    }
}
