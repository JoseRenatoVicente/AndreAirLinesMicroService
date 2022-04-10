using AndreAirLines.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasDapper.API.Repository
{
    public interface IAirportDataRepository
    {
        Task<IEnumerable<AirportData>> GetAllAirportAsync();
        Task<AirportData> GetAirportByIdAsync(string code);
        Task<bool> AddAirportAsync(AirportData airport);
        Task<bool> UpdateAirportAsync(AirportData airport);
        Task<bool> DeleteAirportAsync(string code);
    }
}
