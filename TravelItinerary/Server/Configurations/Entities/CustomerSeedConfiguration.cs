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
                    Name = "Harsh",
                    Alias = "Kunal",
                    PhoneNumber = "82375471",
                    Gender = "Male",
                    Email = "harsh123@gmail.com",
                    Password = "A12345678"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Ying Zheng",
                    Alias = "Tang",
                    PhoneNumber = "88359828",
                    Gender = "Male",
                    Email = "yingzheng678@gmail.com",
                    Password = "S5151675678"
                }
            );
        }
    }
}
