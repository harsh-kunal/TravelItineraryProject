using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelItinerary.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelItinerary.Server.Configurations.Entities
{
    public class CustomerSeedConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Harsh [Default by System]",
                    Alias = "Kunal [Default by System]",
                    PhoneNumber = "82375471",
                    Gender = "Male [Default by System]",
                    Email = "harsh123@gmail.com",
                    Password = "A12345678"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Ying Zheng [Default by System]",
                    Alias = "Tang [Default by System]",
                    PhoneNumber = "88359828",
                    Gender = "Male [Default by System]",
                    Email = "yingzheng678@gmail.com",
                    Password = "S5151675678"
                }
            );
        }
    }
}
