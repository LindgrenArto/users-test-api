using System;
using System.Collections.Generic;
using UsersTestApi.DTOModels;

namespace UsersTestApi.DTOModels
{
    public class AddressDTO
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public GeoDTO Geo { get; set; }
    }
}
