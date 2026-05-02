using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElectroLight.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _db;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext Db)
        {
            _db = Db;
            _dbSet = _db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? Filter = null,
                      bool AsTracking = true,
            params Expression<Func<T, object>>[] Includes)
        {
            IQueryable<T> query = _dbSet;

            if (!AsTracking)
            {
                query = query.AsNoTracking();
            }

            foreach (var includeProp in Includes)
            {
                query = query.Include(includeProp);
            }
            if (Filter != null)
            {
                query = query.Where(Filter);
            }
            return await query.ToListAsync();
        }



        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> Filter,
                      bool AsTracking = true,
            params Expression<Func<T, object>>[] Includes)
        {
            IQueryable<T> query = _dbSet;

            if (!AsTracking)
            {
                query = query.AsNoTracking();
            }

            foreach (var includeProp in Includes)
            {
                query = query.Include(includeProp);
            }

            return await query.FirstOrDefaultAsync(Filter);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        //public async Task SaveChangesAsync()
        //{
        //    await _db.SaveChangesAsync();
        //}

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }



        public async Task<T?> GetBetterVersionAsync(
            Expression<Func<T, bool>> Filter,
            bool AsTracking = true,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (!AsTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(Filter);
        }

        public async Task<IEnumerable<T>> GetAllBetterVersionAsync(
         Expression<Func<T, bool>>? Filter = null,
         bool AsTracking = true,
         Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (!AsTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (Filter != null)
                query = query.Where(Filter);

            return await query.ToListAsync();
        }


        //////////////////////////////////////
        ////////For Future improvements/////
        //////////////////////////////////////

        //public async Task<T?> GetAsync(
        //    Expression<Func<T, bool>> Filter,
        //    bool AsTracking = true,
        //    Func<IQueryable<T>, IQueryable<T>>? include = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (!AsTracking)
        //        query = query.AsNoTracking();

        //    if (include != null)
        //        query = include(query);

        //    return await query.FirstOrDefaultAsync(Filter);
        //}


        //public async Task<IEnumerable<T>> GetAllAsync(
        // Expression<Func<T, bool>>? Filter = null,
        // bool AsTracking = true,
        // Func<IQueryable<T>, IQueryable<T>>? include = null)
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (!AsTracking)
        //        query = query.AsNoTracking();

        //    if (include != null)
        //        query = include(query);

        //    if (Filter != null)
        //        query = query.Where(Filter);

        //    return await query.ToListAsync();
        //}



    }
}
