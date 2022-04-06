using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;

namespace Flights.API.Repository
{
    public interface IFlightRepository : IBaseRepository<Flight>
    {
    }
}
