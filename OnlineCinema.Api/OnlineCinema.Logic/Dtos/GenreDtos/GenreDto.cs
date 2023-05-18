using System;

namespace OnlineCinema.Logic.Dtos.GenreDtos
{
    /// <summary>
    /// DTO модель для представления жанра.
    /// </summary>
    public class GenreDto
    {
        /// <summary>
        /// ID жанра.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование жанра.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// URL изображения жанра
        /// </summary>
        public string? ImageUrl { get; set; } = null!;
    }
}
