using System;
using System.Collections.Generic;
using UsersTestApi.DTOModels;

namespace UsersTestApi.DTOModels
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public AddressDTO? Address { get; set; }
        public CompanyDTO? Company { get; set; }
    }
}