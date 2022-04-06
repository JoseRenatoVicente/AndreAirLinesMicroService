using AndreAirLines.Domain.Entities.Base;
using System;

namespace AndreAirLines.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PercentagePromotion { get; set; }
        public BasePrice BasePrice { get; set; }
        public Class Class { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public decimal PricePromotion()
        {
            return PricePromotion(PercentagePromotion);
        }

        public decimal PricePromotion(decimal percentagePromotion)
        {
            return TotalPrice = percentagePromotion == 0 ? (BasePrice.Value + Class.Value) : (BasePrice.Value + Class.Value) * (percentagePromotion / 100);
        }
    }
}
