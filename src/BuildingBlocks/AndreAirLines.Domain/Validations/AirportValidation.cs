using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class AirportValidation : AbstractValidator<Airport>
    {
        public AirportValidation()
        {
            RuleFor(c => c.IATACode)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(3).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 60).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Address).SetValidator(new AddressValidation());
        }
    }
}
