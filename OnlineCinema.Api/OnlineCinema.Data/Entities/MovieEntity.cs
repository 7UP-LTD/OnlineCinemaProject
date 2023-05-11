using System;

namespace OnlineCinema.Data.Entities
{
    public class MovieEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// дата добавления на сайт
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// год выпуска фильма
        /// </summary>
        public DateTime ReleaseYear { get; set; }

        /// <summary>
        /// ссылка на картинку-заставку
        /// </summary>
        public string MoviePoster { get; set; }

        /// <summary>
        /// сериал или нет
        /// </summary>
        public bool IsSeries { get; set; }
    }
}