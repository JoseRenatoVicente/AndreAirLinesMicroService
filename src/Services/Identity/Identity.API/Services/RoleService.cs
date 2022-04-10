using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations.Identity;
using Identity.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class RoleService : BaseService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository, INotifier notifier) : base(notifier)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync() =>
            await _roleRepository.GetAllAsync();

        public async Task<Role> GetRoleByDescriptionAsync(string description) =>
    await _roleRepository.FindAsync(c => c.Description == description);

        public async Task<Role> GetRoleByIdAsync(string id) =>
            await _roleRepository.FindAsync(c => c.Id == id);

        public async Task<Role> AddAsync(Role role)
        {
            if (!ExecuteValidation(new RoleValidation(), role)) return role;

            return await _roleRepository.AddAsync(role);
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task RemoveAsync(Role roleIn) =>
            await _roleRepository.RemoveAsync(roleIn);

        public async Task RemoveAsync(string id) =>
            await _roleRepository.RemoveAsync(id);
    }
}
