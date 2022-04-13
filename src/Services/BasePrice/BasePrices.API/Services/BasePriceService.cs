using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using AndreAirLines.WebAPI.Core.Notifications;
using BasePrices.API.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasePrices.API.Services
{
    public class BasePriceService : BaseService, IBasePriceService
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

        public async Task<BasePrice> AddBasePriceAsync(BasePrice basePrice)
        {
            if (basePrice.Origin.Id == basePrice.Destination.Id)
            {
                Notification("Origin and Destination cannot be the same");
                return basePrice;
            }

            Airport origin = await _gatewayService.GetFromJsonAsync<Airport>("Airport/api/Airports/" + basePrice.Origin.Id);
            if (origin == null)
            {
                Notification("Origin Airport does not exist registered in our database database");
                return basePrice;
            }

            Airport destination = await _gatewayService.GetFromJsonAsync<Airport>("Airport/api/Airports/" + basePrice.Destination.Id);
            if (destination == null)
            {
                Notification("Destination Airport does not exist registered in our database database");
                return basePrice;
            }

            basePrice.Origin = origin;
            basePrice.Destination = destination;

            if (!ExecuteValidation(new BasePriceValidation(), basePrice)) return basePrice;

            await _gatewayService.PostLogAsync(null, basePrice, Operation.Create);

            return await _basePriceRepository.AddAsync(basePrice);
        }

        public async Task<BasePrice> UpdateBasePriceAsync(BasePrice basePrice)
        {
            var basePriceBefore = await _basePriceRepository.FindAsync(c => c.Id == basePrice.Id);


            if (basePriceBefore == null)
            {
                Notification("Not found");
                return basePrice;
            }

            await _gatewayService.PostLogAsync(basePriceBefore, basePrice, Operation.Update);

            return await _basePriceRepository.UpdateAsync(basePrice);
        }

        public async Task RemoveBasePriceAsync(BasePrice basePrice)
        {
            await _gatewayService.PostLogAsync(basePrice, null, Operation.Delete);

            await _basePriceRepository.RemoveAsync(basePrice);
        }

        public async Task<bool> RemoveBasePriceAsync(string id)
        {
            var basePrice = await _basePriceRepository.FindAsync(c => c.Id == id);

            if (basePrice == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(basePrice, null, Operation.Delete);

            await _basePriceRepository.RemoveAsync(id);

            return true;
        }
    }
}
