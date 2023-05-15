﻿using System;
using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class ChangeMovieRequest
    {
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
        public string MoviePosterUrl { get; set; }

        /// <summary>
        /// сериал или нет
        /// </summary>
        public bool IsSeries { get; set; }

        public string? ContentUrl { get; set; }

        public List<MovieGenreDto> Genres { get; set; } = new();
       
        public List<MovieTagDto> Tags { get; set; } = new();
        
        public List<MovieActorDto> Actors { get; set; } = new();
        
        public List<MovieDirectorDto> Directors { get; set; } = new();
        
        public List<MovieWriterDto> Writers { get; set; } = new();
    }
}