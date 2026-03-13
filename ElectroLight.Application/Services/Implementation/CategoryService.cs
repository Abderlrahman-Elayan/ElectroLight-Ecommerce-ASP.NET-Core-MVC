using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Application.Services.IServices;
using ElectroLight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElectroLight.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(
            Expression<Func<Category, bool>>? filter = null,
            params Expression<Func<Category, object>>[] includes)
        {
            return await _uow.Categories.GetAllAsync(filter, includes);
        }

        public async Task<Category?> GetAsync(Expression<Func<Category, bool>> filter)
        {

            return await _uow.Categories.GetAsync(filter);
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _uow.Categories.AddAsync(category);
            await _uow.SaveChangesAsync();

            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _uow.Categories.Update(category);
            await _uow.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            try
            {
                _uow.Categories.Remove(category);
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
