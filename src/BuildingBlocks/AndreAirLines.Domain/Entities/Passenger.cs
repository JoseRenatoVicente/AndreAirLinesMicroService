using AndreAirLines.Domain.Entities.Base;

namespace AndreAirLines.Domain.Entities
{
    public class Passenger : Person
    {
        public string PassportNumber { get; set; }
        public Passenger()
        {

        }
    }
}
