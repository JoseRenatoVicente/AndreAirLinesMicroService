using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace Aircrafts.API.Repository
{
    public class AircraftRepository : BaseRepository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
