using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using MongoDB.Driver;

namespace BasePrices.API.Infrastructure.Repository
{
    public class BasePriceRepository : BaseRepository<BasePrice>, IBasePriceRepository
    {
        public BasePriceRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
