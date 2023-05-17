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
                // .Include(x => x.Actors)
                // .Include(x => x.Directors)
                // .Include(x => x.Writers)
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

                if (filter.CountryId.HasValue)
                {
                    query = query.Where(x => x.CountryId == filter.CountryId.Value);
                }

                if (filter.ReleaseDate.HasValue)
                {
                    query = query.Where(x => x.ReleaseDate.Year == filter.ReleaseDate.Value.Year);
                }
                
            }

            return await query.ToListAsync();
        }

        public async Task UpdateMovie(Guid id, MovieEntity movie)
        {
            // var movieActors = await _context.MovieActors.Where(x => x.MovieId == id).ToListAsync();
            // _context.MovieActors.RemoveRange(movieActors);
            // var movieDirectors = await _context.MovieDirectors.Where(x => x.MovieId == id).ToListAsync();
            // _context.MovieDirectors.RemoveRange(movieDirectors);
            // var movieWriters = await _context.MovieWriters.Where(x => x.MovieId == id).ToListAsync();
            // _context.MovieWriters.RemoveRange(movieWriters);
            var movieTags = await _context.MovieTags.Where(x => x.MovieId == id).ToListAsync();
            _context.MovieTags.RemoveRange(movieTags);
            var movieGenres = await _context.MovieGenres.Where(x => x.MovieId == id).ToListAsync();
            _context.MovieGenres.RemoveRange(movieGenres);

            var movieEntity = _context.Movies
                // .Include(x => x.Actors)
                // .Include(x => x.Directors)
                // .Include(x => x.Writers)
                .Include(x => x.Tags)
                .Include(x => x.Genres)
                .FirstOrDefault(x => x.Id == id)!;
            if (movieEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _context.Movies.Update(movie);

            // Добавление связанных коллекций
            // _context.MovieActors.AddRange(movie.Actors.Select(x => new MovieActorEntity()
            // {
            //     Id = Guid.NewGuid(),
            //     MovieId = id,
            //     ActorId = x.ActorId
            // }));
            //
            // _context.MovieDirectors.AddRange(movie.Directors.Select(x => new MovieDirectorEntity()
            // {
            //     Id = Guid.NewGuid(),
            //     MovieId = id,
            //     DirectorId = x.DirectorId
            // }));
            //
            // _context.MovieWriters.AddRange(movie.Writers.Select(x => new MovieWriterEntity()
            // {
            //     Id = Guid.NewGuid(),
            //     MovieId = id,
            //     WriterId = x.WriterId
            // }));

            _context.MovieTags.AddRange(movie.Tags.Select(x => new MovieTagEntity()
            {
                Id = Guid.NewGuid(),
                MovieId = id,
                TagId = x.TagId
            }));
            
            _context.MovieGenres.AddRange(movie.Genres.Select(x => new MovieGenreEntity()
            {
                Id = Guid.NewGuid(),
                MovieId = id,
                GenreId = x.GenreId
            }));

            await _context.SaveChangesAsync();
        }
    }
}