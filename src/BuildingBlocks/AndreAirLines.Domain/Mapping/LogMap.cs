using AndreAirLines.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace AndreAirLines.Domain.Mapping
{
    public class LogMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Log>(map =>
            {

            });
        }
    }
}
