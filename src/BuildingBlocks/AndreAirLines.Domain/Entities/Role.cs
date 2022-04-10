using AndreAirLines.Domain.Entities.Base;
using System.Collections.Generic;

namespace AndreAirLines.Domain.Entities
{
    public class Role : EntityBase
    {
        public string Description { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
