using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class SeasonRepository : BaseRepository<MovieSeasonEntity>, ISeasonRepository
    {
        private readonly ApplicationDbContext _context;

        public SeasonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MovieSeasonEntity?> GetSeasonsByMovieId(Guid movieId)
        {
            return await _context.MovieSeasons
                .Include(x => x.Episodes)
                .FirstOrDefaultAsync(x => x.MovieId == movieId);
        }

        public async Task<MovieSeasonEntity?> GetSeasonById(Guid seasonId)
        {
            return await _context.MovieSeasons
                .Include(x => x.Episodes)
                .FirstOrDefaultAsync(x => x.Id == seasonId);
        }

        public async Task UpdateSeason(Guid id, MovieSeasonEntity season)
        {
            var seasonEpisodes = await _context.MovieEpisodes.Where(x => x.SeasonId == id).ToListAsync();
            if (seasonEpisodes != null)
            {
                _context.MovieEpisodes.RemoveRange(seasonEpisodes);
            }


            var seasonEntity = _context.MovieSeasons
                .Include(x => x.Episodes)
                .FirstOrDefault(x => x.Id == id)!;
            if (seasonEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _context.MovieSeasons.Update(season);
            
            // Добавление связанных коллекций
            _context.MovieEpisodes.AddRange(season.Episodes.Select(x => new MovieEpisodeEntity()
            {
                Id = Guid.NewGuid(),
                SeasonId = id,
            }));

            await _context.SaveChangesAsync();
        }
    }
}