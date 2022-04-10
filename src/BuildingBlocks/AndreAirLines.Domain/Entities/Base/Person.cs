using AndreAirLines.API.Models.Enums;
using AndreAirLines.Domain.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace AndreAirLines.Domain.Entities.Base
{
    public abstract class Person : EntityBase
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
