using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class FlightValidation : AbstractValidator<Flight>
    {
        public FlightValidation()
        {
            RuleFor(c => c.DepartureTime)
               .NotEmpty().WithMessage("The {PropertyName} field must be provided");

            RuleFor(c => c.DisembarkationTime)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided");

            RuleFor(c => c.Aircraft).SetValidator(new AircraftValidation());

            RuleFor(c => c.Origin).SetValidator(new AirportValidation());

            RuleFor(c => c.Destination).SetValidator(new AirportValidation());
        }
    }
}
