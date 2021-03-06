using AndreAirLines.Domain.Entities.Base;
using AndreAirLines.WebAPI.Core.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace AndreAirLines.Domain.Services.Base
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notification(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notification(error.ErrorMessage);
            }
        }

        protected void Notification(string message)
        {
            _notifier.Handle(message);
        }

        protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity) where TValidation : AbstractValidator<TEntity> where TEntity : EntityBase
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notification(validator);

            return false;
        }
    }
}