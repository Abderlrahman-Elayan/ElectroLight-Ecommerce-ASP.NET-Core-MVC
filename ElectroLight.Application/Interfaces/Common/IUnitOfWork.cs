using ElectroLight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace ElectroLight.Application.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories{ get; }
        IRepository<Product> Products { get; }
        IRepository<ShoppingCart> ShoppingCarts { get; }
        IRepository<CartItem> CartItems { get; }
        
        Task SaveChangesAsync();

    }
}
