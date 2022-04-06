using AndreAirLines.Domain.Entities.Base;

namespace AndreAirLines.Domain.Entities
{
    public class Aircraft : EntityBase
    {
        public string Registration { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
