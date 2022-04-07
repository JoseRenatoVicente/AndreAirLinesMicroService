using AndreAirLines.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportIngestion.Repository
{
    public interface IAirportRepository
    {
        Task<bool> AddAirport(AirportData airport);
        Task<IEnumerable<AirportData>> GetAllAirport();

    }
}
