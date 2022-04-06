using Aircrafts.API.Repository;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircrafts.API.Services
{
    public class AircraftService : BaseService
    {
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftService(IAircraftRepository aircraftRepository, INotifier notifier) : base(notifier)
        {
            _aircraftRepository = aircraftRepository;
        }

        public async Task<IEnumerable<Aircraft>> GetAircraftsAsync() =>
            await _aircraftRepository.GetAllAsync();

        public async Task<Aircraft> GetAircraftByIdAsync(string id) =>
            await _aircraftRepository.FindAsync(c => c.Id == id);

        public async Task<Aircraft> AddAsync(Aircraft aircraft)
        {
            if (!ExecuteValidation(new AircraftValidation(), aircraft)) return aircraft;

            return await _aircraftRepository.AddAsync(aircraft);
        }

        public async Task<Aircraft> UpdateAsync(Aircraft aircraft)
        {
            return await _aircraftRepository.UpdateAsync(aircraft);
        }

        public async Task RemoveAsync(Aircraft aircraftIn) =>
            await _aircraftRepository.RemoveAsync(aircraftIn);

        public async Task RemoveAsync(string id) =>
            await _aircraftRepository.RemoveAsync(id);
    }
}
