using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Dtos
{
    /// <summary>
    /// Объект страницы с элементами типа T.
    /// </summary>
    /// <typeparam name="T">Тип элементов на странице.</typeparam>
    public class PageDto<T> where T : class
    {
        /// <summary>
        /// Общее количество элементов.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Количество элементов на странице.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

        /// <summary>
        /// Элементы на странице.
        /// </summary>
        public IEnumerable<T>? Items { get; set; }
    }
}
