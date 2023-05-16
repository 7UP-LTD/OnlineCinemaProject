using System;
using System.Collections.Generic;

namespace OnlineCinema.Data.Filters
{
    public class MovieEntityFilter
    {
        public string? Name { get; set; }

        public List<Guid>? Tags { get; set; }

        public List<Guid>? Genres { get; set; }
    }
}