using System;
using System.Collections.Generic;

namespace UsersTestApi.Models
{
    public class UserDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollection { get; set; } = null!;
    }
}