using Aircrafts.API.Repository;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using AndreAirLines.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircrafts.API.Services
{
    public class AircraftService : BaseService
    {
        private readonly GatewayService _gatewayService;
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftService(IAircraftRepository aircraftRepository, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _aircraftRepository = aircraftRepository;
            _gatewayService = gatewayService;
        }

        public async Task<IEnumerable<Aircraft>> GetAircraftsAsync() =>
            await _aircraftRepository.GetAllAsync();

        public async Task<Aircraft> GetAircraftByIdAsync(string id) =>
            await _aircraftRepository.FindAsync(c => c.Id == id);

        public async Task<Aircraft> AddAircraftAsync(Aircraft aircraft)
        {
            if (!ExecuteValidation(new AircraftValidation(), aircraft)) return aircraft;

            await _gatewayService.PostLogAsync(null, aircraft, Operation.Create);

            return await _aircraftRepository.AddAsync(aircraft);
        }

        public async Task<Aircraft> UpdateAircraftAsync(Aircraft aircraft)
        {
            var aircraftBefore = await _aircraftRepository.FindAsync(c => c.Id == aircraft.Id);


            if (aircraftBefore == null)
            {
                Notification("Not found");
                return aircraft;
            }

            await _gatewayService.PostLogAsync(aircraftBefore, aircraft, Operation.Update);

            return await _aircraftRepository.UpdateAsync(aircraft);
        }

        public async Task RemoveAircraftAsync(Aircraft aircraft)
        {
            await _gatewayService.PostLogAsync(aircraft, null, Operation.Delete);

            await _aircraftRepository.RemoveAsync(aircraft);
        }

        public async Task<bool> RemoveAircraftAsync(string id)
        {
            var aircraft = await _aircraftRepository.FindAsync(c => c.Id == id);

            if (aircraft == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(aircraft, null, Operation.Delete);

            await _aircraftRepository.RemoveAsync(id);

            return true;
        }
    }
}
