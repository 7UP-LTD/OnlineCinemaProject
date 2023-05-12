﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Repositories.IRepositories;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineCinema.Data.Repositories
{
    /// <summary>
    /// Реализация базового репозитория <see cref="IBaseRepository{T}"/> для работы с сущностями типа <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseRepository{T}"/>.
        /// </summary>
        /// <param name="db">Контекст базы данных <see cref="ApplicationDbContext"/>.</param>
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null, 
            bool tracked = false, 
            string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
                query = query.AsNoTracking();

            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',',
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);
                
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T?> GetOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null, 
            bool tracked = false, 
            string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',',
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);
            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        /// <inheritdoc/>
        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка сохранения данных: " + ex.Message);
            }
        }
    }
}
