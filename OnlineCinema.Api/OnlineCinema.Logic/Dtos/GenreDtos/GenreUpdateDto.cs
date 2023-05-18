using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.GenreDtos
{
    /// <summary>
    /// DTO модель для обновлаения информации о жанрах.
    /// </summary>
    public class GenreUpdateDto
    {
        /// <summary>
        /// ID жанра.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование жанра.
        /// </summary>
        [Required(ErrorMessage = "Наименование жанра обязательное поле.")]
        [StringLength(50, MinimumLength = 1,
            ErrorMessage = "Длина названия должна быть не меньше 1 и не больше 50 символов.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// URL изображения жанра
        /// </summary>
        public string? ImageUrl { get; set; } = null!;
    }
}