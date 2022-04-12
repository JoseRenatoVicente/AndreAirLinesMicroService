using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.DTO;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseController
    {
        private readonly LogService _logsService;

        public LogsController(INotifier notifier, LogService logsService) : base(notifier)
        {
            _logsService = logsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLog()
        {
            return Ok(await _logsService.GetLogsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogRequest>> GetLog(string id)
        {
            var log = await _logsService.GetLogByIdAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return new LogRequest(log);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLog(string id, Log log)
        {
            if (id != log.Id)
            {
                return BadRequest();
            }

            await _logsService.UpdateLogAsync(log);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<LogRequest>> PostLog(LogRequest log)
        {
            return await CustomResponseAsync(new LogRequest(await _logsService.AddLogAsync(new Log(log))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(string id)
        {
            var log = await GetLog(id);
            if (log == null)
            {
                return NotFound();
            }
            await _logsService.RemoveLogAsync(id);

            return NoContent();
        }
    }
}
