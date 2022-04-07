using AndreAirLines.Domain.Entities.Base;
using System;

namespace AndreAirLines.Domain.Entities
{
    public class Class : EntityBase
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
