using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using Passengers.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passengers.API.Services
{
    public class PassengerService : BaseService
    {
        private readonly GatewayService _gatewayService;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ViaCepService _viaCepService;

        public PassengerService(GatewayService gatewayService, IPassengerRepository passengerRepository, ViaCepService viaCepService, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _passengerRepository = passengerRepository;
            _viaCepService = viaCepService;
        }

        public async Task<IEnumerable<Passenger>> GetPassengersAsync() =>
            await _passengerRepository.GetAllAsync();

        public async Task<Passenger> GetPassengerByIdAsync(string id) =>
            await _passengerRepository.FindAsync(c => c.Id == id);

        public async Task<Passenger> AddAsync(Passenger passenger)
        {
            if (_passengerRepository.FindAsync(c => c.Cpf == passenger.Cpf) != null)
            {
                Notification("There is already a passenger with this document number");
                return passenger;
            }


            var address = await _viaCepService.ConsultarCEP(passenger.Address);
            if (address is not null) passenger.Address = address;

            if (!ExecuteValidation(new PassengerValidation(), passenger)) return passenger;

            var user = new User { LoginUser = passenger.LoginUser };
            await _gatewayService.PostLogAsync(user, null, passenger, Operation.Create);

            return await _passengerRepository.AddAsync(passenger);
        }

        public async Task<Passenger> UpdateAsync(Passenger passenger)
        {

            var passengerBefore = await _passengerRepository.FindAsync(c => c.Id == passenger.Id);


            if (passengerBefore == null)
            {
                Notification("Not found");
                return passenger;
            }

            var user = new User { LoginUser = passenger.LoginUser };
            await _gatewayService.PostLogAsync(user, passengerBefore, passenger, Operation.Update);

            return await _passengerRepository.UpdateAsync(passenger);
        }

        public async Task RemoveAsync(Passenger passengerIn)
        {
            var user = new User { LoginUser = passengerIn.LoginUser };
            await _gatewayService.PostLogAsync(user, passengerIn, null, Operation.Delete);

            await _passengerRepository.RemoveAsync(passengerIn);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var passenger = await _passengerRepository.FindAsync(c => c.Id == id);

            if (passenger == null)
            {
                Notification("Not found");
                return false;
            }

            var user = new User { LoginUser = passenger.LoginUser };
            await _gatewayService.PostLogAsync(user, passenger, null, Operation.Delete);

            await _passengerRepository.RemoveAsync(id);

            return true;
        }
    }
}
