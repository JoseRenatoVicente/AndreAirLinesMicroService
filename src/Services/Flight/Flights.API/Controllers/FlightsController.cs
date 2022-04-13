using AndreAirLines.Domain.Entities;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using Flights.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : BaseController
    {
        private readonly IFlightService _flightsService;

        public FlightsController(IFlightService flightsService, INotifier notifier) : base(notifier)
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

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(string id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            await _flightsService.UpdateFlightAsync(flight);

            return NoContent();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostFlight(Flight flight)
        {
            return await CustomResponseAsync(await _flightsService.AddFlightAsync(flight));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            var flight = await GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            await _flightsService.RemoveFlightAsync(id);

            return NoContent();
        }

    }
}
