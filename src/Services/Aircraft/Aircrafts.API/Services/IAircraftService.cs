using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircrafts.API.Services
{
    public interface IAircraftService
    {
        Task<Aircraft> AddAircraftAsync(Aircraft aircraft);
        Task<Aircraft> GetAircraftByIdAsync(string id);
        Task<IEnumerable<Aircraft>> GetAircraftsAsync();
        Task RemoveAircraftAsync(Aircraft aircraft);
        Task<bool> RemoveAircraftAsync(string id);
        Task<Aircraft> UpdateAircraftAsync(Aircraft aircraft);
    }
}