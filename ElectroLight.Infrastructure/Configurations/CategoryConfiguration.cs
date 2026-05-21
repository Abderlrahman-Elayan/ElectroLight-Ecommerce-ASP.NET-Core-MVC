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
            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasData(
     new Category
     {
         Id = 1,
         Name = "Headsets",
         Description = "High-quality audio devices including gaming and wireless headsets."
     },
     new Category
     {
         Id = 2,
         Name = "Laptops",
         Description = "Portable computers for work, gaming, and everyday use."
     },
     new Category
     {
         Id = 3,
         Name = "Monitors",
         Description = "High-resolution displays for gaming, design, and productivity."
     }
 );
        }
    }
}
