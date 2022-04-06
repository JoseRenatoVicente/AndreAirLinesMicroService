using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using BasePrices.API.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasePrices.API.Services
{
    public class BasePriceService : BaseService
    {
        private readonly GatewayService _gatewayService;
        private readonly IBasePriceRepository _basePriceRepository;

        public BasePriceService(INotifier notifier, GatewayService gatewayService, IBasePriceRepository basePriceRepository) : base(notifier)
        {
            _gatewayService = gatewayService;
            _basePriceRepository = basePriceRepository;
        }

        public async Task<IEnumerable<BasePrice>> GetBasePricesAsync() =>
            await _basePriceRepository.GetAllAsync();

        public async Task<BasePrice> GetBasePriceByIdAsync(string id) =>
            await _basePriceRepository.FindAsync(c => c.Id == id);

        public async Task<BasePrice> GetBasePriceRecentlyAsync(string originAirportId, string destinationAirportId) =>
            await _basePriceRepository.FindAsync(c => c.Origin.Id == originAirportId 
                                                && c.Destination.Id == destinationAirportId);

        public async Task<BasePrice> AddAsync(BasePrice basePrice)
        {

            if (basePrice.Origin.Id == basePrice.Destination.Id)
            {
                Notification("Origin and Destination cannot be the same");
                return basePrice;
            }

            Airport origin = await _gatewayService.GetFromJsonAsync<Airport>($"Airport/api/Airports/{basePrice.Origin.Id}");
            if (origin == null)
            {
                Notification("Origin Airport does not exist registered in our database database");
                return basePrice;
            }

            Airport destination = await _gatewayService.GetFromJsonAsync<Airport>($"Airport/api/Airports/{basePrice.Destination.Id}");
            if (destination == null)
            {
                Notification("Destination Airport does not exist registered in our database database");
                return basePrice;
            }

            basePrice.Origin = origin;
            basePrice.Destination = destination;

            if (!ExecuteValidation(new BasePriceValidation(), basePrice)) return basePrice;

            return await _basePriceRepository.AddAsync(basePrice);
        }

        public async Task<BasePrice> UpdateAsync(BasePrice BasePrice)
        {
            return await _basePriceRepository.UpdateAsync(BasePrice);
        }

        public async Task RemoveAsync(BasePrice BasePriceIn) =>
            await _basePriceRepository.RemoveAsync(BasePriceIn);

        public async Task RemoveAsync(string id) =>
            await _basePriceRepository.RemoveAsync(id);
    }
}
