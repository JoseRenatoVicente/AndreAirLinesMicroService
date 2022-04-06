using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace BasePrices.API.Infrastructure.Repository
{
    public class BasePriceRepository : BaseRepository<BasePrice>, IBasePriceRepository
    {
        public BasePriceRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
