using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class BasePriceValidation : AbstractValidator<BasePrice>
    {
        public BasePriceValidation()
        {
            RuleFor(c => c.Value)
                    .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");


            RuleFor(c => c.Origin).SetValidator(new AirportValidation());

            RuleFor(c => c.Destination).SetValidator(new AirportValidation());

        }
    }
}