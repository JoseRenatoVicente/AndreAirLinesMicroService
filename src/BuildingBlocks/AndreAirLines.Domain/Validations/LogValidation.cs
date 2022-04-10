using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Validations.Identity;
using FluentValidation;

namespace AndreAirLines.Domain.Validations
{
    public class LogValidation : AbstractValidator<Log>
    {
        public LogValidation()
        {
           
        }
    }
}
