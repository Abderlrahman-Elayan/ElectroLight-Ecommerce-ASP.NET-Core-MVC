using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Domain.Entities;
using ElectroLight.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace ElectroLight.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IRepository<Category> Categories { get; private set; }
        public IRepository<Product> Products{ get; private set; }
        public IRepository<ShoppingCart> ShoppingCarts { get; private set; }
        public IRepository<CartItem> CartItems { get; private set; }

        public IRepository<Order> Orders{ get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }


        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new Repository<Category>(_db);

            Products = new Repository<Product>(_db);

            ShoppingCarts = new Repository<ShoppingCart>(_db);

            CartItems = new Repository<CartItem>(_db);

            Orders = new Repository<Order>(_db);

            OrderItems = new Repository<OrderItem>(_db);

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
