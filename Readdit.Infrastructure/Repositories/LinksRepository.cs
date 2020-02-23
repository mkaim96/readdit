using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Repositories
{
    public class LinksRepository : ILinksRepository
    {
        private ApplicationDbContext _context;

        public LinksRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Link> Add(Link link)
        {
            await _context.AddAsync(link);
            await _context.SaveChangesAsync();
            return link;
        }

        public async Task<IReadOnlyList<Link>> GetAll()
        {
            return await _context.Links
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<Link> GetLinkWithCommentsById(int id)
        {
            return await _context.Links
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .Include(x => x.User)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Link> GetById(int id)
        {
            return await _context.Links.FindAsync(id);
        }
    }
}
