namespace AndreAirLines.Domain.Entities
{
    public class Address
    {
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }

        public Address()
        {

        }

        public Address(string district, string city, string cEP, string street, string state)
        {
            District = district;
            City = city;
            CEP = cEP;
            Street = street;
            State = state;
        }
    }
}
