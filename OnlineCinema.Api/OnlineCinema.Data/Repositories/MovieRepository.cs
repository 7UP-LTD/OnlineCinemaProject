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

        public async Task UpdateMovie(Guid id, MovieEntity movie)
        {
        //     var movieActors = await _context.MovieActors.Where(x => x.MovieId == id).ToList();
        //     await _context.MovieActors.RemoveRange(movieActors);
        //     var movieDirectors = await _context.MovieDirectors.Where(x => x.MovieId == id).ToList();
        //     await _context.MovieDirectors.RemoveRange(movieDirectors);
        //     var movieWriters = await _context.MovieWriters.Where(x => x.MovieId == id).ToList();
        //     await _context.MovieWriters.RemoveRange(movieWriters);
        //     var movieGenres = await _context.MovieGenres.Where(x => x.MovieId == id).ToList();
        //     await _context.MovieGenres.RemoveRange(movieGenres);
        //
        //     var movieEntity = await _context.Movies
        //         .Include(x => x.Actors)
        //         .Include(x => x.Directors)
        //         .Include(x => x.Writers)
        //         .Include(x => x.Tags)
        //         .FirstOrDefault(x => x.Id == id)!;
        //     if (movieEntity == null)
        //     {
        //         //_logger.LogError("Not found movie by id: {Id}", id);
        //         throw new ArgumentException("Not found");
        //     }
        //
        //     await _context.Movies.Update(movieEntity);
        //
        //     // // Добавление связанных коллекций
        //     // _context.Frequencies.AddRange(habit.DayNumbers.Select(x => new FrequencyEntity
        //     // {
        //     //     Id = Guid.NewGuid(),
        //     //     HabitId = id,
        //     //     DayNumber = x
        //     // }));
        //
        //
        //     _context.SaveChanges();
         }

    }


}