using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using System.Text.Json;

namespace AndreAirLines.Domain.DTO
{
    public class LogRequest
    {
        public string Id { get; set; }
        public User User { get; set; }
        public object EntityBefore { get; set; }
        public object EntityAfter { get; set; }
        public Operation Operation { get; set; }

        public LogRequest()
        {

        }

        public LogRequest(Log log)
        {
            User = log.User;
            EntityBefore = JsonSerializer.Deserialize<object>(log.EntityBefore);
            EntityAfter = JsonSerializer.Deserialize<object>(log.EntityAfter);
            Operation = log.Operation;
        }
    }
}
