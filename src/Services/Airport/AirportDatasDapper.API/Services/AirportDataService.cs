using AirportDatasDapper.API.Repository;
using AndreAirLines.Domain.DTO;
using System;
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

        public async Task<IEnumerable<AirportData>> GetAirportsAsync()
        {
            return await _airportRepository.GetAllAirportAsync();
        }
        public async Task<AirportData> GetAirportAsync(string code)
        {
            return await _airportRepository.GetAirportByIdAsync(code);
        }

        public async Task<bool> AddAirportAsync(AirportData airport)
        {
            return await _airportRepository.AddAirportAsync(airport);
        }

        public async Task<bool> UpdateAirportAsync(AirportData airport)
        {
            return await _airportRepository.UpdateAirportAsync(airport);
        }

        public async Task<bool> RemoveAirportAsync(string code)
        {
            return await _airportRepository.DeleteAirportAsync(code);
        }
    }
}
