using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Filters;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class MovieRepository : BaseRepository<MovieEntity>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MovieEntity?> GetMovieById(Guid movieId)
        {
            return await _context.Movies
                .Include(x => x.Seasons)
                .ThenInclude(x => x.Episodes)
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Include(x => x.Writers)
                .FirstOrDefaultAsync(x => x.Id == movieId);
        }
        
        public async Task<List<MovieEntity>> GetPagedMovies(int page, int pageSize, MovieEntityFilter? filter = null)
        {
            var query = _context.Movies
                .OrderBy(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(o => o.Name.Contains(filter.Name));
                }
                
                if (filter.Genres != null && filter.Genres.Count > 0)
                {
                    query = query.Where(o => o.Genres.Any(g => filter.Genres.Contains(g.GenreId)));
                }
                
                if (filter.Tags != null && filter.Tags.Count > 0)
                {
                    query = query.Where(o => o.Tags.Any(t => filter.Tags.Contains(t.TagId)));
                }
            }

            return await query.ToListAsync();
        }
    }
}