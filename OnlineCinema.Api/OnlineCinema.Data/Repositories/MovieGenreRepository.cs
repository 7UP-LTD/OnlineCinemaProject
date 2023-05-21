using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class MovieGenreRepository : BaseRepository<MovieGenreEntity>, IMovieGenreRepository
    {
        public MovieGenreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}