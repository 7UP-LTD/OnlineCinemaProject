using System;
using System.Collections.Generic;

namespace OnlineCinema.Logic.Dtos
{
    public class MovieFilter
    {
        public string? Name { get; set; }

        public List<Guid>? Tags { get; set; }

        public List<Guid>? Genres { get; set; }

        public Guid? CountryId { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}