using ElectroLight.Application.Utilies;
using ElectroLight.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Infrastructure.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                 new IdentityRole
                 {
                     Id = "1",
                     Name = SD.Role_Admin,
                     NormalizedName = SD.Role_Admin.ToUpper(),
                     ConcurrencyStamp = "1"
                 },
                 new IdentityRole
                 {
                     Id = "2",
                     Name = SD.Role_Customer,
                     NormalizedName = SD.Role_Customer.ToUpper(),
                         ConcurrencyStamp = "2" 
                 }
             );
        }
    }
}