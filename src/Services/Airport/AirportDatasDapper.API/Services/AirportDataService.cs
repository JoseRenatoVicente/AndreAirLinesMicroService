using AirportDatasDapper.API.Repository;
using AndreAirLines.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasDapper.Services
{
    public class AirportDataService
    {
        private readonly IAirportDataRepository _airportRepository;
        public AirportDataService(IAirportDataRepository airportDataRepository)
        {
            _airportRepository = airportDataRepository;
        }

        public async Task<bool> AddAirportAsync(AirportData airport)
        {
            return await _airportRepository.AddAirport(airport);
        }

        public async Task<IEnumerable<AirportData>> GetAirportsAsync()
        {
            return await _airportRepository.GetAllAirport();
        }
    }
}
