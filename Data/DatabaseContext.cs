using ezCloth.Data.Configuration;
using ezCloth.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezCloth.Data
{
    public class DatabaseContext: IdentityDbContext<SystemUsers>
    {
        // Create a DB Context here
        public DatabaseContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
