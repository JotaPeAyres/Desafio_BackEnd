using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Models.Validations
{
    public class DeliveryManValidation :AbstractValidator<DeliveryMan>
    {
        public DeliveryManValidation() 
        { 
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("{PropertyName} field is required")
                .Length(2, 200).WithMessage("The size of the {PropertyName} field must be between {MinLength} and {MaxLength}");
            
            RuleFor(f => DocumentValidation.ValidateCnpj(f.Document)).Equal(true)
                .WithMessage("The {PropetyName} is invalid");
            RuleFor(f => f.Document)
                .NotEmpty().WithMessage("{PropertyName} field is required")
                .Length(14, 14).WithMessage("The size of the {PropertyName} field must have {MaxLength}");

            RuleFor(f => f.Birthdate)
                .NotEmpty().WithMessage("{PropertyName} field is required");

            RuleFor(f => f.LicenseNumber)
                .NotEmpty().WithMessage("{PropertyName} field is required");

        }
    }
}
