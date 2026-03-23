using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Application.Services.IServices;
using ElectroLight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElectroLight.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }
         public async Task<IEnumerable<Product>> GetAllAsync(
            Expression<Func<Product, bool>>? filter = null,
            bool AsTracking = true,
            params Expression<Func<Product, object>>[] includes)
        {
            return await _uow.Products.GetAllAsync(filter, AsTracking, Includes: includes);
        }

        public async Task<Product?> GetAsync(Expression<Func<Product, bool>> filter,bool AsTracking = true, params Expression<Func<Product, object>>[] includes)
        {

            return await _uow.Products.GetAsync(filter, AsTracking, includes);
        }

        public async Task<Product> AddAsync(Product Product)
        {
            await _uow.Products.AddAsync(Product);
            await _uow.SaveChangesAsync();

            return Product;
        }

        public async Task UpdateAsync(Product Product)
        {
            _uow.Products.Update(Product);
            await _uow.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Product Product)
        {
            try
            {
                _uow.Products.Remove(Product);
                await _uow.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
