using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    /// <summary>
    /// DTO модель для передачи краткой информация о фильме.
    /// </summary>
    public class ShortInfoMovieDto
    {
        /// <summary>
        /// Уникальный идентификатор фильма.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название фильма.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Дата выхода фильма.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// URL постера фильма.
        /// </summary>
        public string? MoviePosterUrl { get; set; }
    }
}
