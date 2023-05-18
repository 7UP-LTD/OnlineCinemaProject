using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineCinema.Data.Entities;

namespace OnlineCinema.Data.Repositories.IRepositories
{
    public interface IEpisodeRepository : IBaseRepository<MovieEpisodeEntity>
    {
        Task<List<MovieEpisodeEntity>> GetEpisodesBySeasonId(Guid seasonId);
        
        Task<MovieEpisodeEntity> GetEpisodeById(Guid episodeId);
        
        Task UpdateEpisode(Guid id, MovieEpisodeEntity episode);
    }
}