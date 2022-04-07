using AirportDatasEF.API.Repository;
using AndreAirLines.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasEF.API.Services
{
    public class AirportDataService
    {
        private readonly IAirportDataRepository _airportDataRepository;
        public AirportDataService(IAirportDataRepository airportDataRepository)
        {
            _airportDataRepository = airportDataRepository;
        }

        public async Task<IEnumerable<AirportData>> GetAirportDatasAsync()
        {
            return await _airportDataRepository.GetAirportDatasAsync();
        }

        public async Task<IEnumerable<AirportData>> GetAirportDatasRawAsync()
        {
            return await _airportDataRepository.GetAirportDatasRawAsync();
        }

        public async Task AddAirportDataAsync(AirportData airportData)
        {
            await _airportDataRepository.AddAirportDataAsync(airportData);
        }

        public async Task AddAirportDataRawAsync(AirportData airportData)
        {
            await _airportDataRepository.AddAirportDataRawAsync(airportData);
        }
    }
}
