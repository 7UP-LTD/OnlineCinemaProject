using System;
using System.Threading.Tasks;
using OnlineCinema.Data.Entities;

namespace OnlineCinema.Data.Repositories.IRepositories
{
    public interface ISeasonRepository : IBaseRepository<MovieSeasonEntity>
    {
        Task<MovieSeasonEntity?> GetSeasonsByMovieId(Guid movieId);

        Task<MovieSeasonEntity> GetSeasonById(Guid seasonId);
        Task UpdateSeason(Guid id, MovieSeasonEntity seasonEntity);
    }
}