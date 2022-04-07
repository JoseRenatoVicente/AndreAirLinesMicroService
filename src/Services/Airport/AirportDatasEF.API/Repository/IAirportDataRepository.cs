using AndreAirLines.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportDatasEF.API.Repository
{
    public interface IAirportDataRepository
    {
        Task<IEnumerable<AirportData>> GetAirportDatasAsync();
        Task<IEnumerable<AirportData>> GetAirportDatasRawAsync();
        Task AddAirportDataAsync(AirportData airportData);
        Task AddAirportDataRawAsync(AirportData airportData);
    }
}
