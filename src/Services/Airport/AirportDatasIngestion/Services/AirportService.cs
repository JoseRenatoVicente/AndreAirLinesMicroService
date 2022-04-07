using AirportIngestion.Repository;
using AndreAirLines.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportIngestion.Services
{
    public class AirportService
    {
        private readonly IAirportRepository _airportRepository;
        public AirportService()
        {
            _airportRepository = new AirportRepository();
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
