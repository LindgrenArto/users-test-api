using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UsersTestApi.Models
{
    public class Geo
    {
        [BsonElement("lat")]
        public string Lat { get; set; }

        [BsonElement("lng")]
        public string Lng { get; set; }
    }
}