using AndreAirLines.Domain.Entities;
using FluentValidation;

namespace AndreAirLines.Domain.Validations.Identity
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(f => f.Cpf.Length).Equal(ValidationCPF.LenghtCpf)
                .WithMessage("The CPF field must have {ComparisonValue} characters and has been supplied {PropertyValue}.");
            RuleFor(f => ValidationCPF.ValidarCPF(f.Cpf)).Equal(true)
                .WithMessage("The CPF provided is invalid.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(2, 60).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(6, 10).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Sex).NotNull()
                .WithMessage("The {PropertyName} field must be provided");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(s => s.Password)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided")
                .Length(8, 30).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Address).SetValidator(new AddressValidation());

            RuleFor(c => c.Role).NotEmpty().WithMessage("The {PropertyName} must be provided");

        }
    }
}
