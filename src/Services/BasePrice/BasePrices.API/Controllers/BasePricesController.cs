using AndreAirLines.Domain.Entities;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using BasePrices.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasePrices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasePricesController : BaseController
    {
        private readonly IBasePriceService _basePricesService;

        public BasePricesController(IBasePriceService basePricesService, INotifier notifier) : base(notifier)
        {
            _basePricesService = basePricesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasePrice>>> GetBasePrice()
        {
            return Ok(await _basePricesService.GetBasePricesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasePrice>> GetBasePrice(string id)
        {
            var basePrice = await _basePricesService.GetBasePriceByIdAsync(id);

            if (basePrice == null)
            {
                return NotFound();
            }

            return basePrice;
        }

        [HttpGet("{originAirportId}/{destinationAirportId}")]
        public async Task<ActionResult<BasePrice>> GetBasePrice(string originAirportId, string destinationAirportId)
        {
            var basePrice = await _basePricesService.GetBasePriceRecentlyAsync(originAirportId, destinationAirportId);

            if (basePrice == null)
            {
                return NotFound();
            }

            return basePrice;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasePrice(string id, BasePrice basePrice)
        {
            if (id != basePrice.Id) return BadRequest();

            await _basePricesService.UpdateBasePriceAsync(basePrice);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostBasePrice(BasePrice basePrice)
        {
            return await CustomResponseAsync(await _basePricesService.AddBasePriceAsync(basePrice));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasePrice(string id)
        {
            var basePrice = await GetBasePrice(id);
            if (basePrice == null)
            {
                return NotFound();
            }
            await _basePricesService.RemoveBasePriceAsync(id);

            return NoContent();
        }

    }
}
