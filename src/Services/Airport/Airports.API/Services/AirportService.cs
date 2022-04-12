using Airports.API.Repository;
using AndreAirLines.Domain.DTO;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
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
        private readonly GatewayService _gatewayService;
        private readonly IAirportRepository _airportRepository;

        public AirportService(INotifier notifier, ViaCepService viaCepService, GatewayService gatewayService, IAirportRepository airportRepository) : base(notifier)
        {
            _viaCepService = viaCepService;
            _gatewayService = gatewayService;
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<Airport>> GetAirportsAsync() =>
            await _airportRepository.GetAllAsync();

        public async Task<Airport> GetAirportByIdAsync(string id) =>
            await _airportRepository.FindAsync(c => c.Id == id);

        public async Task<Airport> AddAirportAsync(Airport airport)
        {
            var airportData = await _gatewayService.GetFromJsonAsync<AirportData>("AirportDatasDapper/api/AirportDatas/" + airport.IATACode);
            if (airportData != null) airport.SetAirportData(airportData);

            var address = await _viaCepService.ConsultarCEP(airport.Address);
            if (address != null) airport.Address = address;

            if (!ExecuteValidation(new AirportValidation(), airport)) return airport;

            await _gatewayService.PostLogAsync(null, airport, Operation.Create);

            return await _airportRepository.AddAsync(airport);
        }

        public async Task<Airport> UpdateAirportAsync(Airport airport)
        {
            var airportBefore = await _airportRepository.FindAsync(c => c.Id == airport.Id);

            if (airportBefore == null)
            {
                Notification("Not found");
                return airport;
            }

            await _gatewayService.PostLogAsync(airportBefore, airport, Operation.Update);

            return await _airportRepository.UpdateAsync(airport);
        }

        public async Task RemoveAirportAsync(Airport airport)
        {
            await _gatewayService.PostLogAsync(airport, null, Operation.Delete);

            await _airportRepository.RemoveAsync(airport);
        }

        public async Task<bool> RemoveAirportAsync(string id)
        {
            var airport = await _airportRepository.FindAsync(c => c.Id == id);

            if (airport == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(airport, null, Operation.Delete);

            await _airportRepository.RemoveAsync(id);

            return true;
        }
    }
}
