using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelItinerary.Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelItinerary.Server.Configurations.Entities
{
    public class AccountSeedConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser
                {
                    Email = "admininstrartor@localhost.com",
                    UserName = "Admin",
                    FirstName = "Admin",
                    LastName = "Localhost",
                    PasswordHash = "P@ssword",
                    NormalizedUserName = "Administrator"
                }
             ) ;
        }
    }
}
