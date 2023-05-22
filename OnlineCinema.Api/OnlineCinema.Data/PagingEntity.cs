using System;
using System.Collections.Generic;

namespace OnlineCinema.Data.Repositories
{
    /// <summary>
    /// Класс, представляющий страницу сущностей с пагинацией.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class PagingEntity<TEntity> where TEntity : class
    {
        /// <summary>
        /// Общее количество сущностей.
        /// </summary>
        public int TotalTEntity { get; set; }

        /// <summary>
        /// Список сущностей на текущей странице.
        /// </summary>
        public IEnumerable<TEntity> TEntities { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Конструктор класса PagingEntity.
        /// </summary>
        /// <param name="items">Список сущностей на текущей странице.</param>
        /// <param name="totalEntity">Общее количество сущностей.</param>
        /// <param name="entityPerPage">Количество сущностей на странице.</param>
        public PagingEntity(IEnumerable<TEntity> items, int totalEntity, int entityPerPage)
        {
            TEntities = items;
            TotalTEntity = totalEntity;
            TotalPages = (int)Math.Ceiling((decimal)TotalTEntity / entityPerPage);
        }
    }
}
