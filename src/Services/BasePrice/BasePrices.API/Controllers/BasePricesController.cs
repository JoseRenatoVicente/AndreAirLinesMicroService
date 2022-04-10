using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using BasePrices.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasePrices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasePricesController : BaseController
    {
        private readonly BasePriceService _basePricesService;

        public BasePricesController(INotifier notifier, BasePriceService basePricesService) : base(notifier)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasePrice(string id, BasePrice basePrice)
        {
            if (id != basePrice.Id) return BadRequest();

            await _basePricesService.UpdateAsync(basePrice);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostBasePrice(BasePrice basePrice)
        {
            return await CustomResponseAsync(await _basePricesService.AddAsync(basePrice));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasePrice(string id)
        {
            var basePrice = await GetBasePrice(id);
            if (basePrice == null)
            {
                return NotFound();
            }
            await _basePricesService.RemoveAsync(id);

            return NoContent();
        }

    }
}
