using ElectroLight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasData(
            new Category { Id = 1, Name = "Phones" },
            new Category { Id = 2, Name = "Laptops" },
            new Category { Id = 3, Name = "Screens" }
        );
        }
    }
}
