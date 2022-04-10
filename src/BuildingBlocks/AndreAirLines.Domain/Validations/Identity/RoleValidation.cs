using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations.Identity
{
    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 60).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");
        }
    }
}
