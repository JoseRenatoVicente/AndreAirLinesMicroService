using AirportDatasDapper.Services;
using AndreAirLines.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasDapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportDatasController : ControllerBase
    {
        private readonly AirportDataService _airportDataService;

        public AirportDatasController(AirportDataService airportDataService)
        {
            _airportDataService = airportDataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportData>>> GetAirportData()
        {
            return Ok(await _airportDataService.GetAirportsAsync());
        }

        [HttpPost]
        public async Task<ActionResult<AirportData>> PostAirportData(AirportData airportData)
        {
            await _airportDataService.AddAirportAsync(airportData);
            return airportData;
        }

    }
}
