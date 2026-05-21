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
                   .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price).HasPrecision(18, 2);

            builder.Property(u => u.CreatedAt)
      .HasDefaultValueSql("GETUTCDATE()")
      .ValueGeneratedOnAdd();


            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasData(
        new Product
        {
            Id = 1,
            Name = "Sony WH-1000XM5 Headphones",
            Description = "Premium noise-cancelling wireless headphones with industry-leading sound quality and comfort.",
            Price = 299.99m,
            StockQuantity = 80,
            CategoryId = 1,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        },
        new Product
        {
            Id = 2,
            Name = "SteelSeries Arctis 7",
            Description = "Wireless gaming headset with surround sound and low-latency performance.",
            Price = 149.99m,
            StockQuantity = 60,
            CategoryId = 1 ,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        },

        new Product
        {
            Id = 3,
            Name = "Dell XPS 15",
            Description = "High-performance laptop with Intel i7 processor and stunning OLED display.",
            Price = 1799.99m,
            StockQuantity = 25,
            CategoryId = 2 ,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        },
        new Product
        {
            Id = 4,
            Name = "MacBook Air M2",
            Description = "Lightweight and powerful laptop with Apple M2 chip for productivity and development.",
            Price = 1199.99m,
            StockQuantity = 40,
            CategoryId = 2 ,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        },

        new Product
        {
            Id = 5,
            Name = "LG UltraGear 27''",
            Description = "27-inch gaming monitor with 144Hz refresh rate and 1ms response time.",
            Price = 349.99m,
            StockQuantity = 50,
            CategoryId = 3 ,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        },
        new Product
        {
            Id = 6,
            Name = "Samsung Odyssey G7",
            Description = "Curved QHD gaming monitor with ultra-smooth performance and HDR support.",
            Price = 599.99m,
            StockQuantity = 35,
            CategoryId = 3 ,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        }
    );
        }
    }
}

