using System;
using System.Collections.Generic;
using UsersTestApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UsersTestApi.Models
{
    public class Address
    {
        [BsonElement("street")]
        public string Street { get; set; }

        [BsonElement("suite")]
        public string Suite { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("zipcode")]
        public string Zipcode { get; set; }

        [BsonElement("geo")]
        public Geo Geo { get; set; }
    }
}