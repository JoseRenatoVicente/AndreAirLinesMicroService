using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace Passengers.API.Repository
{
    public class PassengerRepository : BaseRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
