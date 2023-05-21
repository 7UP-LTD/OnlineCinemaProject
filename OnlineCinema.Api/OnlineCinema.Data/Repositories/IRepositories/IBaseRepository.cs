using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineCinema.Data.Repositories.IRepositories
{
    /// <summary>
    /// Интерфейс базового репозитория.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Получает список сущностей из контекста данных, удовлетворяющую условиям фильтрации.
        /// </summary>
        /// <param name="filter">Выражение фильтрации.</param>
        /// <param name="includeProperty">Строка, указывающая, какие свойства навигации должны быть включены в запрос.</param>
        /// <returns>Cписок сущностей из контекста данных, удовлетворяющую условиям фильтрации.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
                                         string? includeProperty = null,
                                         bool tracked = false);

        /// <summary>
        /// Получает сущность из контекста данных, удовлетворяющую условиям фильтрации, или возвращает null, если сущность не найдена.
        /// </summary>
        /// <param name="filter">Выражение фильтрации.</param>
        /// <param name="tracked">True, если нужно отслеживать изменения сущности.</param>
        /// <param name="includeProperty">Строка, указывающая, какие свойства навигации должны быть включены в запрос.</param>
        /// <returns>Сущность из контекста данных, удовлетворяющую условиям фильтрации, или null, если сущность не найдена.</returns>
        Task<T?> GetOrDefaultAsync(Expression<Func<T, bool>>? filter = null,
                                   bool tracked = false,
                                   string? includeProperty = null);

        /// <summary>
        /// Асинхронно получает страницу сущностей с использованием пагинации и фильтрации.
        /// </summary>
        /// <param name="filter">Выражение для фильтрации сущностей.</param>
        /// <param name="includeProperty">Строка, содержащая имена свойств, которые следует включить в запрос.</param>
        /// <param name="currentPage">Текущая страница.</param>
        /// <returns>Объект <see cref="PagingEntity"/>, содержащий список сущностей на текущей странице, 
        /// общее количество сущностей, текущую страницу и количество элементов на странице.</returns>
        Task<PagingEntity<T>> GetPageEntitiesAsync(Expression<Func<T, bool>>? filter = null,
                                                   string? includeProperty = null,
                                                   int currentPage = 1,
                                                   int tEntityPerPage = 100);

        /// <summary>
        /// Добавляет и сохраняет новую сущность в контекст данных.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Удаляет сущность из контекста данных и сохраняет изменения.
        /// </summary>
        /// <param name="entity">Удаляемая сущность.</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Обнавляет сущность в контексте данных и сохраняет изменения.
        /// </summary>
        /// <param name="entity">Обновляемая сущность.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Сохраняет изменения в контексте базы данных
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
