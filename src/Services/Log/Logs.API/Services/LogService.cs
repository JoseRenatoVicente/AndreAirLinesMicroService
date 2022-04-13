using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using AndreAirLines.WebAPI.Core.Notifications;
using Logs.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Services
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository, INotifier notifier) : base(notifier)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<Log>> GetLogsAsync() =>
            await _logRepository.GetAllAsync();

        public async Task<Log> GetLogByIdAsync(string id) =>
            await _logRepository.FindAsync(c => c.Id == id);

        public async Task<Log> AddLogAsync(Log log)
        {
            if (!ExecuteValidation(new LogValidation(), log)) return log;

            return await _logRepository.AddAsync(log);
        }

        public async Task<Log> UpdateLogAsync(Log log)
        {
            return await _logRepository.UpdateAsync(log);
        }

        public async Task RemoveLogAsync(Log log) =>
            await _logRepository.RemoveAsync(log);

        public async Task RemoveLogAsync(string id) =>
            await _logRepository.RemoveAsync(id);

    }
}
