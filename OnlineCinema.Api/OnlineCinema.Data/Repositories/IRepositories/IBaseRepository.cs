using System.Linq.Expressions;

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
        /// <param name="tracked">True, если нужно отслеживать изменения сущности.</param>
        /// <param name="includeProperty">Строка, указывающая, какие свойства навигации должны быть включены в запрос.</param>
        /// <returns>Cписок сущностей из контекста данных, удовлетворяющую условиям фильтрации.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
                                         bool tracked = false,
                                         string? includeProperty = null);

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
