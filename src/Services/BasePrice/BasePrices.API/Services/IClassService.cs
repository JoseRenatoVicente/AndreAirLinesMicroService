using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classs.API.Services
{
    public interface IClassService
    {
        Task<Class> AddClassAsync(Class @class);
        Task<Class> GetClassByIdAsync(string id);
        Task<IEnumerable<Class>> GetClasssAsync();
        Task RemoveClassAsync(Class @class);
        Task<bool> RemoveClassAsync(string id);
        Task<Class> UpdateClassAsync(Class @class);
    }
}