using Aircrafts.API.Services;
using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircrafts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : BaseController
    {
        private readonly AircraftService _aircraftsService;

        public AircraftsController(AircraftService aircraftsService, INotifier notifier) : base(notifier)
        {
            _aircraftsService = aircraftsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aircraft>>> GetAircraft()
        {
            return Ok(await _aircraftsService.GetAircraftsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aircraft>> GetAircraft(string id)
        {
            var aircraft = await _aircraftsService.GetAircraftByIdAsync(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            return aircraft;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAircraft(string id, Aircraft aircraft)
        {
            if (id != aircraft.Id)
            {
                return BadRequest();
            }

            await _aircraftsService.UpdateAircraftAsync(aircraft);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Aircraft>> PostAircraft(Aircraft aircraft)
        {
            return await CustomResponseAsync(await _aircraftsService.AddAircraftAsync(aircraft));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAircraft(string id)
        {
            var aircraft = await GetAircraft(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            await _aircraftsService.RemoveAircraftAsync(id);

            return NoContent();
        }
    }
}
