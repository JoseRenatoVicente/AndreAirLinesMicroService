using AndreAirLines.Domain.Entities.Base;
using System;

namespace AndreAirLines.Domain.Entities
{
    public class Flight : EntityBase
    {
        public Airport Destination { get; set; }
        public Airport Origin { get; set; }
        public Aircraft Aircraft { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DisembarkationTime { get; set; }
    }
}
