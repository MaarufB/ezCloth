using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezCloth.Data.Configuration
{
    public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Name = "Individual",
                    NormalizedName = "INDIVIDUAL"
                },
                new IdentityRole
                {
                    Name = "Seller",
                    NormalizedName = "SELLER"
                }
                );
        }

    }
}
