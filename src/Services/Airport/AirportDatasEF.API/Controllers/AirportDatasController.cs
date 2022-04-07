using AirportDatasEF.API.Services;
using AndreAirLines.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatas.API.Controllers
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

        [HttpGet("EF")]
        public async Task<ActionResult<IEnumerable<AirportData>>> GetAirportData()
        {
            return Ok(await _airportDataService.GetAirportDatasAsync());
        }

        [HttpGet("EFRaw")]
        public async Task<ActionResult<IEnumerable<AirportData>>> GetAirportDataRaw()
        {
            return Ok(await _airportDataService.GetAirportDatasRawAsync());
        }

        [HttpPost("EF")]
        public async Task<ActionResult<AirportData>> PostAirportData(AirportData airportData)
        {
            await _airportDataService.AddAirportDataAsync(airportData);
            return airportData;
        }

        [HttpPost("EFRaw")]
        public async Task<ActionResult<AirportData>> PostAirportDataRaw(AirportData airportData)
        {
            await _airportDataService.AddAirportDataAsync(airportData);
            return airportData;
        }
    }
}
