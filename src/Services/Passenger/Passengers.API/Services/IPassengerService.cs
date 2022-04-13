using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passengers.API.Services
{
    public interface IPassengerService
    {
        Task<Passenger> AddPassengerAsync(Passenger passenger);
        Task<Passenger> GetPassengerByIdAsync(string id);
        Task<IEnumerable<Passenger>> GetPassengersAsync();
        Task RemovePassengerAsync(Passenger passenger);
        Task<bool> RemovePassengerAsync(string id);
        Task<Passenger> UpdatePassengerAsync(Passenger passenger);
    }
}