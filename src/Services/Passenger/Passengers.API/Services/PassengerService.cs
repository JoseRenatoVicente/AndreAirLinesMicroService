using AndreAirLines.Domain.Entities;
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
        private readonly IPassengerRepository _passengerRepository;
        private readonly ViaCepService _viaCepService;

        public PassengerService(IPassengerRepository passengerRepository, ViaCepService viaCepService, INotifier notifier) : base(notifier)
        {
            _passengerRepository = passengerRepository;
            _viaCepService = viaCepService;
        }

        public async Task<IEnumerable<Passenger>> GetPassengersAsync() =>
            await _passengerRepository.GetAllAsync();

        public async Task<Passenger> GetPassengerByIdAsync(string id) =>
            await _passengerRepository.FindAsync(c => c.Id == id);

        public async Task<Passenger> AddAsync(Passenger passenger)
        {
            var address = await _viaCepService.ConsultarCEP(passenger.Address);
            if (address is not null) passenger.Address = address;
            
            if (!ExecuteValidation(new PassengerValidation(), passenger)) return passenger;

            return await _passengerRepository.AddAsync(passenger);
        }

        public async Task<Passenger> UpdateAsync(Passenger passenger)
        {
            return await _passengerRepository.UpdateAsync(passenger);
        }

        public async Task RemoveAsync(Passenger passengerIn) =>
            await _passengerRepository.RemoveAsync(passengerIn);

        public async Task RemoveAsync(string id) =>
            await _passengerRepository.RemoveAsync(id);
    }
}
