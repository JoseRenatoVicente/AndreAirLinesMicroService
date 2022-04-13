using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.API.Services
{
    public interface IFlightService
    {
        Task<Flight> AddFlightAsync(Flight flight);
        Task<Flight> GetFlightByIdAsync(string id);
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task RemoveFlightAsync(Flight flight);
        Task<bool> RemoveFlightAsync(string id);
        Task<Flight> UpdateFlightAsync(Flight flight);
    }
}