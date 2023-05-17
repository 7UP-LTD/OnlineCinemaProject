using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.TagDtos
{
    /// <summary>
    /// DTO модель для тегов.
    /// </summary>
    public class TagDto
    {
        /// <summary>
        /// ID тега.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование тега. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Наименование обязательное поле.")]
        public string Name { get; set; } = null!;
    }
}
