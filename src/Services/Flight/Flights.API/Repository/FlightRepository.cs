using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using MongoDB.Driver;

namespace Flights.API.Repository
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        public FlightRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
