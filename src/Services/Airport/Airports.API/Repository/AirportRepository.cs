using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace Airports.API.Repository
{
    public class AirportRepository : BaseRepository<Airport>, IAirportRepository
    {
        public AirportRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
