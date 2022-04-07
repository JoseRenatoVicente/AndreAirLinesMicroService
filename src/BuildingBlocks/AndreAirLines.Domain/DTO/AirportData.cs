using System;

namespace AndreAirLines.Domain.DTO
{
    public class AirportData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Continent { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"City: {City}\n" +
                $"Country: {Country}\n" +
                $"Code: {Code}\n" +
                $"Continent: {Continent}\n";

        }
    }
}
