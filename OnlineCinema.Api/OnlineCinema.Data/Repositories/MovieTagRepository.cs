using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class MovieTagRepository : BaseRepository<MovieTagEntity>, IMovieTagRepository
    {
        public MovieTagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}