using AndreAirLines.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasDapper.API.Repository
{
    public interface IAirportDataRepository
    {
        Task<bool> AddAirport(AirportData airport);
        Task<IEnumerable<AirportData>> GetAllAirport();
    }
}
