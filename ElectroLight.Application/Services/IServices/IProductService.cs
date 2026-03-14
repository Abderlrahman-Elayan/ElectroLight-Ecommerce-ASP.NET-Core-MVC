using ElectroLight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElectroLight.Application.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>>? filter = null, params Expression<Func<Product, object>>[] includes);
        Task<Product?> GetAsync(Expression<Func<Product, bool>> filter);
        Task<Product> AddAsync(Product Product);
        Task UpdateAsync(Product Product);
        Task<bool> DeleteAsync(Product product);
    }
}
