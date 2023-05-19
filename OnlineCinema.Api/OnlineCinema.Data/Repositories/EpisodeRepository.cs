using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class EpisodeRepository : BaseRepository<MovieEpisodeEntity>, IEpisodeRepository

    {
        private readonly ApplicationDbContext _context;

        public EpisodeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MovieEpisodeEntity>> GetEpisodesBySeasonId(Guid seasonId)
        {
            return await _context.MovieEpisodes
                .Include(x => x.Comments)
                .Where(x => x.SeasonId == seasonId).ToListAsync();
        }

        public async Task<MovieEpisodeEntity?> GetEpisodeById(Guid episodeId)
        {
            return await _context.MovieEpisodes
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == episodeId);
        }

        public async Task UpdateEpisode(Guid id, MovieEpisodeEntity episode)
        {
            // var episodeComments = await _context.EpisodeComments.Where(x => x.EpisodeId == id).ToListAsync();
            // if (episodeComments != null) _context.EpisodeComments.RemoveRange(episodeComments);


            var episodeEntity = _context.MovieEpisodes
                .FirstOrDefault(x => x.Id == id)!;
            if (episodeEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _context.MovieEpisodes.Update(episode);

            // Добавление связанных коллекций
            // _context.EpisodeComments.AddRange(episode.Comments.Select(x => new EpisodeCommentEntity()
            // {
            //     Id = Guid.NewGuid(),
            //     EpisodeId = id,
            //     UserId = x.UserId
            // }));

            await _context.SaveChangesAsync();
        }
    }
}