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

        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new Repository<Category>(_db);
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
