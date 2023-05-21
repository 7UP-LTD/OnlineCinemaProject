using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Repositories.IRepositories;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineCinema.Data.Repositories
{
    /// <summary>
    /// Реализация базового репозитория <see cref="IBaseRepository{T}"/> для работы с сущностями типа <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
    public partial class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseRepository{T}"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных <see cref="ApplicationDbContext"/>.</param>
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperty = null,
            bool tracked = false)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
                query = query.AsNoTracking();

            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<PagingEntity<T>> GetPageEntitiesAsync(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperty = null,
            int currentPage = 1, 
            int tEntityPerPage = 100)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (filter != null)
                query = query.Where(filter);

            var totalEntity = query.Count();
            if (tEntityPerPage > 0)
            {
                if (tEntityPerPage > 100)
                    tEntityPerPage = 100;

                query = query.Skip(tEntityPerPage * (currentPage - 1)).Take(tEntityPerPage);
            }

            var tEntityItems = await query.ToListAsync();
            return new PagingEntity<T>(tEntityItems, totalEntity, tEntityPerPage);
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

            if (includeProperty != null)
                foreach (var includeProp in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (filter != null)
                query = query.Where(filter);

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
