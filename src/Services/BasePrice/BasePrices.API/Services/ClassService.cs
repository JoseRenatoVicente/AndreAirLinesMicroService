using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using AndreAirLines.WebAPI.Core.Notifications;
using BasePrices.API.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classs.API.Services
{
    public class ClassService : BaseService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository, INotifier notifier) : base(notifier)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> GetClasssAsync() =>
            await _classRepository.GetAllAsync();

        public async Task<Class> GetClassByIdAsync(string id) =>
            await _classRepository.FindAsync(c => c.Id == id);

        public async Task<Class> AddClassAsync(Class @class)
        {
            if (!ExecuteValidation(new ClassValidation(), @class)) return @class;

            return await _classRepository.AddAsync(@class);
        }

        public async Task<Class> UpdateClassAsync(Class @class)
        {
            return await _classRepository.UpdateAsync(@class);
        }

        public async Task RemoveClassAsync(Class @class) =>
            await _classRepository.RemoveAsync(@class);

        public async Task RemoveClassAsync(string id) =>
            await _classRepository.RemoveAsync(id);
    }
}
