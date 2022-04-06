using AndreAirLines.Domain.Notifications;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Controllers.Base
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ICollection<string> _errors = new List<string>();
        protected readonly INotifier _notifier;
        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected async Task<ActionResult> CustomResponseAsync(object result = null)
        {
            if (await IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                success = false,
                errors = await GetErrors(),
            });
        }


        protected async Task<ActionResult> CustomResponseAsync(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                await AddError(erro.ErrorMessage);
            }

            return await CustomResponseAsync();
        }

        protected async Task<ActionResult> CustomResponseAsync(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                await AddError(erro.ErrorMessage);
            }

            return await CustomResponseAsync();
        }

        protected Task<bool> IsOperationValid()
        {
            return Task.Run(() => !_notifier.IsNotified());
        }

        protected async Task<IEnumerable<string>> GetErrors()
        {
            foreach (var item in _notifier.GetNotifications())
            {
                await AddError(item);
            }
            _notifier.Clear();
            return _errors;
        }
        protected Task AddError(string error)
        {
            return Task.Run(() => _errors.Add(error));
        }
        protected Task ClearErrors()
        {
            return Task.Run(() => _notifier.Clear());
        }

    }
}
