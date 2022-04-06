using AndreAirLines.Domain.Entities.Base;
using System;

namespace AndreAirLines.Domain.Entities
{
    public class BasePrice : EntityBase
    {
        public Airport Destination { get; set; }
        public Airport Origin { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
