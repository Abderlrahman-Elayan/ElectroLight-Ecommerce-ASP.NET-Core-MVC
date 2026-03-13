using ElectroLight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElectroLight.Application.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includes);
        Task<Category?> GetAsync(Expression<Func<Category,bool>> filter);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
    }
}
