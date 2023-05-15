using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Logic.Dtos.TagDtos
{
    public class TagCreateDto
    {
        /// <summary>
        /// Наименование тега. Обязательное поле.
        /// </summary>
        [Required(ErrorMessage = "Наименование обязательное поле.")]
        public string Name { get; set; } = null!;
    }
}
