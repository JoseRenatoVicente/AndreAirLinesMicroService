using Aircrafts.API.Repository;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircrafts.API.Services
{
    public class AircraftService : BaseService
    {
        private readonly GatewayService _gatewayService;
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

            var user = new User { LoginUser = aircraft.LoginUser };
            await _gatewayService.PostLogAsync(user, null, aircraft, Operation.Create);

            return await _aircraftRepository.AddAsync(aircraft);
        }

        public async Task<Aircraft> UpdateAsync(Aircraft aircraft)
        {
            var aircraftBefore = await _aircraftRepository.FindAsync(c => c.Id == aircraft.Id);


            if (aircraftBefore == null)
            {
                Notification("Not found");
                return aircraft;
            }

            var user = new User { LoginUser = aircraft.LoginUser };
            await _gatewayService.PostLogAsync(user, aircraftBefore, aircraft, Operation.Update);

            return await _aircraftRepository.UpdateAsync(aircraft);
        }

        public async Task RemoveAsync(Aircraft aircraftIn)
        {
            var user = new User { LoginUser = aircraftIn.LoginUser };
            await _gatewayService.PostLogAsync(user, aircraftIn, null, Operation.Delete);

            await _aircraftRepository.RemoveAsync(aircraftIn);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var aircraft = await _aircraftRepository.FindAsync(c => c.Id == id);

            if (aircraft == null)
            {
                Notification("Not found");
                return false;
            }

            var user = new User { LoginUser = aircraft.LoginUser };
            await _gatewayService.PostLogAsync(user, aircraft, null, Operation.Delete);

            await _aircraftRepository.RemoveAsync(id);

            return true;
        }
    }
}
