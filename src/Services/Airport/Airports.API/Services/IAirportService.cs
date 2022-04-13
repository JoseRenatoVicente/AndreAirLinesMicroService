using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airports.API.Services
{
    public interface IAirportService
    {
        Task<Airport> AddAirportAsync(Airport airport);
        Task<Airport> GetAirportByIdAsync(string id);
        Task<IEnumerable<Airport>> GetAirportsAsync();
        Task RemoveAirportAsync(Airport airport);
        Task<bool> RemoveAirportAsync(string id);
        Task<Airport> UpdateAirportAsync(Airport airport);
    }
}