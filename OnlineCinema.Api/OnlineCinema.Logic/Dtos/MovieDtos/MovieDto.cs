﻿using System;
using System.Collections.Generic;
using OnlineCinema.Logic.Dtos.DicDtos;
using OnlineCinema.Logic.Models;

namespace OnlineCinema.Logic.Dtos.MovieDtos
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// год выпуска фильма
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// ссылка на картинку-заставку
        /// </summary>
        public string MoviePosterUrl { get; set; }

        public string? MovieBannerUrl { get; set; }

        /// <summary>
        /// сериал или нет
        /// </summary>
        public bool IsSeries { get; set; }

        public string? ContentUrl { get; set; }

        public Guid CountryId { get; set; }

        public DicCountryDto Country { get; set; }

        public int AgeLimit { get; set; }

        public int Duration { get; set; }

        public List<MovieSeasonDto> Seasons { get; set; } = new();

        public List<MovieCommentDto> Comments { get; set; } = new();

        public List<MovieGenreDto> Genres { get; set; } = new();

        public List<MovieTagDto> Tags { get; set; } = new();
    }
}