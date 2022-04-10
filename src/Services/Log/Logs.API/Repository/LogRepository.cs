using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using MongoDB.Driver;

namespace Logs.API.Repository
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
