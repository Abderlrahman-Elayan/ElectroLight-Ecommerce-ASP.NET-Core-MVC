using ElectroLight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace ElectroLight.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories{ get; }

        Task SaveChangesAsync();

    }
}
