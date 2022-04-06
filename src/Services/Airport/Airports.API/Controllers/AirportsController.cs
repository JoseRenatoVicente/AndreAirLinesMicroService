using Airports.API.Services;
using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirport(string id, Airport airport)
        {
            if (id != airport.Id)
            {
                return BadRequest();
            }

            await _airportsService.UpdateAsync(airport);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Airport>> PostAirport(Airport airport)
        {
           return await CustomResponseAsync(await _airportsService.AddAsync(airport));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(string id)
        {
            var passageiro = await GetAirport(id);
            if (passageiro == null)
            {
                return NotFound();
            }
            await _airportsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
