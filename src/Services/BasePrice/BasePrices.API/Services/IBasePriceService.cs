using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasePrices.API.Services
{
    public interface IBasePriceService
    {
        Task<BasePrice> AddBasePriceAsync(BasePrice basePrice);
        Task<BasePrice> GetBasePriceByIdAsync(string id);
        Task<BasePrice> GetBasePriceRecentlyAsync(string originAirportId, string destinationAirportId);
        Task<IEnumerable<BasePrice>> GetBasePricesAsync();
        Task RemoveBasePriceAsync(BasePrice basePrice);
        Task<bool> RemoveBasePriceAsync(string id);
        Task<BasePrice> UpdateBasePriceAsync(BasePrice basePrice);
    }
}