using ElectroLight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ElectroLight.Infrastructure.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(u => u.CreatedAt)
         .HasDefaultValueSql("GETUTCDATE()")
         .ValueGeneratedOnAdd();

            builder.HasOne(c => c.User)
                .WithOne(u => u.ShoppingCart)
                .HasForeignKey<ShoppingCart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.cartItems)
                .WithOne(ci => ci.ShoppingCart)
                .HasForeignKey(ci => ci.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.UserId)
            .IsUnique();
        }
    }
}
