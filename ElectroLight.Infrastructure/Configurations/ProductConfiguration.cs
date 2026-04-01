using ElectroLight.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId);
            //.OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

            builder.HasData(
               new Product
               {
                   Id = 1,
                   Name = "Smartphone",
                   Description = "A high-end smartphone with a sleek design and powerful features.",
                   Price = 699.99m,
                   StockQuantity = 50,
                   CategoryId = 1,
                   CreatedAt = new DateTime(2026, 03, 29, 0, 0, 0, DateTimeKind.Utc)
               },
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Description = "A lightweight laptop with a long battery life, perfect for work and entertainment.",
                    Price = 999.99m,
                    StockQuantity = 30,
                    CategoryId = 1,
                    CreatedAt = new DateTime(2026, 03, 29, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 3,
                    Name = "Headphones",
                    Description = "Noise-cancelling headphones with superior sound quality and comfort.",
                    Price = 199.99m,
                    StockQuantity = 100,
                    CategoryId = 2,
                    CreatedAt = new DateTime(2026, 03, 29, 0, 0, 0, DateTimeKind.Utc)
                });
        }
    }
}

