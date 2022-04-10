using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class AircraftValidation : AbstractValidator<Aircraft>
    {
        public AircraftValidation()
        {

            RuleFor(c => c.Registration)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 60).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 60).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Capacity)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");
        }
    }
}
