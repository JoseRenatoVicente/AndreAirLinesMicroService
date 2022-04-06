using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Flights.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : BaseController
    {
        private readonly FlightService _flightsService;

        public FlightsController(FlightService flightsService, INotifier notifier) : base(notifier)
        {
            _flightsService = flightsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlight()
        {
            return Ok(await _flightsService.GetFlightsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(string id)
        {
            var flight = await _flightsService.GetFlightByIdAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(string id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            await _flightsService.UpdateAsync(flight);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostFlight(Flight flight)
        {
            return await CustomResponseAsync(await _flightsService.AddAsync(flight));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            var flight = await GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            await _flightsService.RemoveAsync(id);

            return NoContent();
        }

    }
}
