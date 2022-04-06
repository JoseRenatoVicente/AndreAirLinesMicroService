using AndreAirLines.API.Models.Enums;
using AndreAirLines.Domain.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace AndreAirLines.Domain.Entities
{
    public class Passenger : EntityBase
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]  // System.Text.Json.Serialization
        [BsonRepresentation(BsonType.String)]
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public Passenger()
        {

        }
    }
}
