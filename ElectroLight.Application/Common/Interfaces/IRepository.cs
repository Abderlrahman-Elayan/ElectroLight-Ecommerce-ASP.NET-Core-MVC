using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElectroLight.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? Filter = null, params Expression<Func<T, object>>[] Includes);
        Task<T> GetAsync(Expression<Func<T, bool>> Filter, params Expression<Func<T, object>>[] Includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        //Task SaveChangesAsync();
    }
}
