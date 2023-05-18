using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Entities;
using OnlineCinema.Data.Repositories.IRepositories;

namespace OnlineCinema.Data.Repositories
{
    public class TagRepository : BaseRepository<DicTagEntity>, ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DicTagEntity?> GetTagByName(string tagName)
        {
            return await _context.DicTags.FirstOrDefaultAsync(x => x.Name.ToUpper() == tagName.ToUpper());
        }
    }
}