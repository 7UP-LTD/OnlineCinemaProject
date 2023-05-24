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
                .Include(x => x.Tags)
                .Include(x => x.Genres)
                .Include(x => x.Comments)
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
                    query = query.Where(o => o.Genres.Any(g => filter.Genres.Contains(g.DicGenreId)));
                }

                if (filter.Tags != null && filter.Tags.Count > 0)
                {
                    query = query.Where(o => o.Tags.Any(t => filter.Tags.Contains(t.TagId)));
                }

                if (filter.CountryId.HasValue)
                {
                    query = query.Where(x => x.CountryId == filter.CountryId.Value);
                }

                if (filter.ReleaseDate.HasValue)
                {
                    query = query.Where(x => x.ReleaseDate.Year == filter.ReleaseDate.Value.Year);
                }

                if (filter.IsSeries.HasValue)
                {
                    query = query.Where(x => x.IsSeries == filter.IsSeries.Value);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<List<Guid>> GetTopGenres(int topRows)
        {
            return await _context.UserMovieLikes
                .Include(x => x.Movie)
                .ThenInclude(x => x.Genres)
                .Where(x => x.IsLike)
                .Select(x => new
                {
                    isLike = x.IsLike,
                    GenreId = x.Movie.Genres.FirstOrDefault().DicGenreId
                })
                .GroupBy(x => x.GenreId)
                .Select(g => new
                {
                    GenreId = g.Key,
                    LikesCount = g.Count()
                })
                .OrderByDescending(x => x.LikesCount)
                .Take(topRows)
                .Select(x => x.GenreId)
                .ToListAsync();
        }

        public async Task<List<MovieEntity>> GetTopMovies(int topRows, Guid? genreId)
        {
            return await _context.UserMovieLikes
                .Include(x => x.Movie)
                .Where(x => x.IsLike
                            && (!genreId.HasValue
                                || x.Movie.Genres.Select(g => g.DicGenreId).Contains(genreId.Value)
                            )
                )
                .GroupBy(x => x.Movie)
                .Select(g => new
                {
                    Movie = g.Key,
                    LikesCount = g.Count()
                })
                .OrderByDescending(x => x.LikesCount)
                .Take(topRows)
                .Select(x => x.Movie)
                .ToListAsync();
        }

        public async Task<List<MovieEntity>> GetTopUserMovies(Guid? userId, int topRows)
        {
            return await _context.UserMovieLikes
                .Include(x => x.Movie)
                .ThenInclude(x => x.Genres)
                .Where(x =>
                    x.UserId == userId
                    && x.IsLike
                )
                .GroupBy(x => x.Movie)
                .Select(g => new
                {
                    Movie = g.Key,
                    LikesCount = g.Count()
                })
                .OrderByDescending(x => x.LikesCount)
                .Take(topRows)
                .Select(x => x.Movie)
                .ToListAsync();
        }
    }
}