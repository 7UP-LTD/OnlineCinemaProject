using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.GenreDtos
{
    /// <summary>
    /// DTO модель для создания жанра.
    /// </summary>
    public class GenreCreateDto
    {
        /// <summary>
        /// Наименование жанра. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Наименование жанра обязательное поле.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина названия должна быть не меньше 1 и не больше 50 символов.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание жанра. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Описание жанра обязательное поле.")]
        public string Description { get; set; } = null!;
    }
}
