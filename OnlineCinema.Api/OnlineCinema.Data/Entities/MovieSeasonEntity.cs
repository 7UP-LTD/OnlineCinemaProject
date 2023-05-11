﻿using System;
using System.Collections.Generic;

namespace OnlineCinema.Data.Entities
{
    public class MovieSeasonEntity : BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        
        /// <summary>
        /// дата добавления на сайт
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// дата выпуска сезона
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        public Guid MovieId { get; set; }
        
        public List<MovieEpisodeEntity> Episodes { get; set; } = new();
    }
}