using AndreAirLines.Domain.DTO;
using AndreAirLines.Domain.Entities.Base;

namespace AndreAirLines.Domain.Entities
{
    public class Airport : EntityBase
    {
        public string IATACode { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public void SetAirportData(AirportData airportData)
        {
            Address.City = airportData.City;
            Address.Country = airportData.Country;
            Address.Continent = airportData.Continent;
        }

    }
}
