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

        public async Task<SubReaddit> Add(SubReaddit subReaddit)
        {
            await _context.SubReaddits.AddAsync(subReaddit);
            await _context.SaveChangesAsync();
            return subReaddit;

        }

        public async Task<bool> Exists(string name)
        {
           return await _context.SubReaddits.AnyAsync(x => x.Name == name);
        }

        public async Task<SubReaddit> GetById(int id)
        {
            return await _context.SubReaddits.FindAsync(id);
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
