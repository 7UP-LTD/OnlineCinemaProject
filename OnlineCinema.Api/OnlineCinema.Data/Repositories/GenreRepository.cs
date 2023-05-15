using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class GenreRepository : BaseRepository<DicGenreEntity>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
