using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private ApplicationDbContext _context;

        public VotesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Vote vote)
        {
            await _context.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AlreadyVoted(int linkId, string userId)
        {
            return await _context.Votes.Where(x => x.Link.Id == linkId && x.User.Id == userId).AnyAsync();
        }

        public async Task<Vote> GetByLinkId(int id)
        {
            return await _context.Votes.FirstAsync(x => x.Link.Id == id);
        }

        public async Task Update(Vote vote)
        {
            await _context.SaveChangesAsync();
        }
    }
}
