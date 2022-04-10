using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using AndreAirLines.Domain.Settings;

namespace Logs.API.Repository
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(IAppSettings appSettings) : base(appSettings)
        {
        }
    }
}
