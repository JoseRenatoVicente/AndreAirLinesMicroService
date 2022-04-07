using AndreAirLines.Domain.Entities.Base;

namespace AndreAirLines.Domain.Entities
{
    public class Airport : EntityBase
    {
        public string IATACode { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

    }
}
