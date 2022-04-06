using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace BasePrices.API.Infrastructure.Repository
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
