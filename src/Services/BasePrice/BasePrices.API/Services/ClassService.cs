using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using AndreAirLines.WebAPI.Core.Notifications;
using BasePrices.API.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classs.API.Services
{
    public class ClassService : BaseService, IClassService
    {
        private readonly GatewayService _gatewayService;
        private readonly IClassRepository _classRepository;

        public ClassService(GatewayService gatewayService, IClassRepository classRepository, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> GetClasssAsync() =>
            await _classRepository.GetAllAsync();

        public async Task<Class> GetClassByIdAsync(string id) =>
            await _classRepository.FindAsync(c => c.Id == id);

        public async Task<Class> AddClassAsync(Class @class)
        {
            if (!ExecuteValidation(new ClassValidation(), @class)) return @class;

            await _gatewayService.PostLogAsync(null, @class, Operation.Create);

            return await _classRepository.AddAsync(@class);
        }

        public async Task<Class> UpdateClassAsync(Class @class)
        {
            var classBefore = await _classRepository.FindAsync(c => c.Id == @class.Id);


            if (classBefore == null)
            {
                Notification("Not found");
                return @class;
            }

            await _gatewayService.PostLogAsync(classBefore, @class, Operation.Update);

            return await _classRepository.UpdateAsync(@class);
        }

        public async Task RemoveClassAsync(Class @class)
        {
            await _gatewayService.PostLogAsync(@class, null, Operation.Delete);

            await _classRepository.RemoveAsync(@class);
        }

        public async Task<bool> RemoveClassAsync(string id)
        {
            var basePrice = await _classRepository.FindAsync(c => c.Id == id);

            if (basePrice == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(basePrice, null, Operation.Delete);

            await _classRepository.RemoveAsync(id);

            return true;
        }

    }
}
