using Airports.API.Services;
using AndreAirLines.Domain.Entities;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airports.API.Controllers
{
    [Route("api/[controller]")]
    public class AirportsController : BaseController
    {
        private readonly AirportService _airportsService;

        public AirportsController(INotifier notifier, AirportService airportsService) : base(notifier)
        {
            _airportsService = airportsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAirport()
        {
            return Ok(await _airportsService.GetAirportsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Airport>> GetAirport(string id)
        {
            var airport = await _airportsService.GetAirportByIdAsync(id);

            if (airport == null)
            {
                return NotFound();
            }

            return airport;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirport(string id, Airport airport)
        {
            if (id != airport.Id)
            {
                return BadRequest();
            }

            await _airportsService.UpdateAirportAsync(airport);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Airport>> PostAirport(Airport airport)
        {
            return await CustomResponseAsync(await _airportsService.AddAirportAsync(airport));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(string id)
        {
            var airport = await GetAirport(id);
            if (airport == null)
            {
                return NotFound();
            }
            await _airportsService.RemoveAirportAsync(id);

            return NoContent();
        }
    }
}
