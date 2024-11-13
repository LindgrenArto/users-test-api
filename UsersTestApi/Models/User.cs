using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UsersTestApi.Models;

namespace UsersTestApi.Models
{
    public class User
    {

        [BsonId] // MongoDB _id field
        [BsonRepresentation(BsonType.ObjectId)] // Specify the objectId.
        public string? MongoId { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("website")]
        public string Website { get; set; }

        [BsonElement("company")]
        public Company Company { get; set; }
    }
}