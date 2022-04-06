using Airports.API.Repository;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airports.API.Services
{
    public class AirportService : BaseService
    {
        private readonly ViaCepService _viaCepService;
        private readonly IAirportRepository _airportRepository;

        public AirportService(INotifier notifier, ViaCepService viaCepService, IAirportRepository airportRepository) : base(notifier)
        {
            _viaCepService = viaCepService;
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<Airport>> GetAirportsAsync() =>
            await _airportRepository.GetAllAsync();

        public async Task<Airport> GetAirportByIdAsync(string id) =>
            await _airportRepository.FindAsync(c => c.Id == id);

        public async Task<Airport> AddAsync(Airport airport)
        {
            var address = await _viaCepService.ConsultarCEP(airport.Address);
            if (address is not null) airport.Address = address;

            if (!ExecuteValidation(new AirportValidation(), airport)) return airport;

            return await _airportRepository.AddAsync(airport);
        }

        public async Task<Airport> UpdateAsync(Airport airport)
        {
            return await _airportRepository.UpdateAsync(airport);
        }

        public async Task RemoveAsync(Airport airportIn) =>
            await _airportRepository.RemoveAsync(airportIn);

        public async Task RemoveAsync(string id) =>
            await _airportRepository.RemoveAsync(id);
    }
}
