using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Validations;
using Logs.API.Repository;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Services
{
    public class LogService : GatewayService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository, HttpClient httpClient, INotifier notifier) : base(httpClient, notifier)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<Log>> GetLogsAsync() =>
            await _logRepository.GetAllAsync();

        public async Task<Log> GetLogByIdAsync(string id) =>
            await _logRepository.FindAsync(c => c.Id == id);

        public async Task<Log> AddAsync(Log log)
        {
            if (!ExecuteValidation(new LogValidation(), log)) return log;

            return await _logRepository.AddAsync(log);
        }

        public async Task<Log> UpdateAsync(Log log)
        {
            return await _logRepository.UpdateAsync(log);
        }

        public async Task RemoveAsync(Log logIn) =>
            await _logRepository.RemoveAsync(logIn);

        public async Task RemoveAsync(string id) =>
            await _logRepository.RemoveAsync(id);

    }
}
