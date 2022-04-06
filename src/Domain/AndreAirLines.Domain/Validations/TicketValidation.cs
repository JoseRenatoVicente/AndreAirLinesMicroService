using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class TicketValidation : AbstractValidator<Ticket>
    {
        public TicketValidation()
        {
            RuleFor(c => c.TotalPrice)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");

            RuleFor(c => c.Flight).SetValidator(new FlightValidation());

            RuleFor(c => c.BasePrice).SetValidator(new BasePriceValidation());

            RuleFor(c => c.Passenger).SetValidator(new PassengerValidation());

            RuleFor(c => c.Class).SetValidator(new ClassValidation());


        }
    }
}
