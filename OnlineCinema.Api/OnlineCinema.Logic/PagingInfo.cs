using System;

namespace OnlineCinema.Logic
{
    /// <summary>
    /// Класс, представляющий информацию о постраничной навигации.
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Oбщее количество элементов.
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// Количество элементов на странице.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Общее количество страниц на основе общего количества элементов и количества элементов на странице.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

    }
}