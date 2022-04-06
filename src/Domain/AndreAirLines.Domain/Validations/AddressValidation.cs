using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {

            RuleFor(c => c.District)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(0, 40).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(3, 60).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(8).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 4).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 40).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 40).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(1, 10).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");
        }
    }
}
