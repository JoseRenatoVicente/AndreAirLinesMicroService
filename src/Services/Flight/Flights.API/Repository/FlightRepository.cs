using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace Flights.API.Repository
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        public FlightRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
