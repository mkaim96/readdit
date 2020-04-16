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
    public class SubReadditRepository : ISubReadditRepository
    {
        private readonly ApplicationDbContext _context;

        public SubReadditRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<SubReaddit>> GetPopular()
        {
            return await _context.SubReaddits.ToListAsync();
        }

        public async Task<IReadOnlyCollection<SubReaddit>> Search(string search)
        {
            return await _context.SubReaddits
                .Where(x => x.Name.ToLower().Contains(search.ToLower()))
                .ToListAsync();
        }
    }
}
