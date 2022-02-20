using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezCloth.Entities
{
    // You need to inherit the IndentityUser Class or else the whe you use Identity User on DbContext/Database Context we will have an error
    public class SystemUsers:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
