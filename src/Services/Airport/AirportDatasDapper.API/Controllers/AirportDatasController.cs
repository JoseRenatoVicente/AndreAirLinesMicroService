using AirportDatasDapper.Services;
using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.DTO;
using AndreAirLines.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasDapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportDatasController : BaseController
    {
        private readonly AirportDataService _airportDataService;

        public AirportDatasController(AirportDataService airportDataService, INotifier notifier) : base(notifier)
        {
            _airportDataService = airportDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportData>>> GetAirportData()
        {
            return Ok(await _airportDataService.GetAirportsAsync());
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<AirportData>> GetAirportData(string code)
        {
            var airport = await _airportDataService.GetAirportAsync(code);

            if (airport == null)
            {
                return NotFound();
            }

            return airport;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAircraft(string code, AirportData airportData)
        {
            if (code != airportData.Code)
            {
                return BadRequest();
            }

            await _airportDataService.UpdateAirportAsync(airportData);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AirportData>> PostAirport(AirportData airportData)
        {
            return await CustomResponseAsync(await _airportDataService.AddAirportAsync(airportData));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(string code)
        {
            var airport = await GetAirportData(code);
            if (airport == null)
            {
                return NotFound();
            }

            await _airportDataService.RemoveAirportAsync(code);

            return NoContent();
        }

    }
}
