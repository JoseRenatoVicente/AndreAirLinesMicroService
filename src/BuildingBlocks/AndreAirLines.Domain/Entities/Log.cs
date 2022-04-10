using AndreAirLines.Domain.DTO;
using AndreAirLines.Domain.Entities.Base;
using AndreAirLines.Domain.Entities.Enums;
using System;
using System.Text.Json;

namespace AndreAirLines.Domain.Entities
{
    public class Log : EntityBase
    {
        public User User { get; set; }
        public string EntityBefore { get; set; }
        public string EntityAfter { get; set; }
        public Operation Operation { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Log()
        {

        }

        public Log(LogRequest logRequest)
        {
            User = logRequest.User;
            EntityBefore = JsonSerializer.Serialize(logRequest.EntityBefore);
            EntityAfter = JsonSerializer.Serialize(logRequest.EntityAfter);
            Operation = logRequest.Operation;
        }
    }
}
