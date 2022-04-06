using Aircrafts.API.Services;
using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircrafts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : BaseController
    {
        private readonly AircraftService _AircraftsService;

        public AircraftsController(AircraftService AircraftsService, INotifier notifier) : base(notifier)
        {
            _AircraftsService = AircraftsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aircraft>>> GetAircraft()
        {
            return Ok(await _AircraftsService.GetAircraftsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aircraft>> GetAircraft(string id)
        {
            var Aircraft = await _AircraftsService.GetAircraftByIdAsync(id);

            if (Aircraft == null)
            {
                return NotFound();
            }

            return Aircraft;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAircraft(string id, Aircraft Aircraft)
        {
            if (id != Aircraft.Id)
            {
                return BadRequest();
            }

            await _AircraftsService.UpdateAsync(Aircraft);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Aircraft>> PostAircraft(Aircraft Aircraft)
        {
            return await CustomResponseAsync(await _AircraftsService.AddAsync(Aircraft));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            var passageiro = await GetAircraft(id);
            if (passageiro == null)
            {
                return NotFound();
            }
            await _AircraftsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
