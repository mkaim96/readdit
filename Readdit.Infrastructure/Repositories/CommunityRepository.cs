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
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Community> Add(Community community)
        {
            await _context.Communities.AddAsync(community);
            await _context.SaveChangesAsync();
            return community;

        }

        public async Task<bool> Exists(string name)
        {
           return await _context.Communities.AnyAsync(x => x.Name == name);
        }

        public async Task<Community> GetByName(string name)
        {
            return await _context.Communities.FirstAsync(x => x.Name == name);
        }

        public async Task<IReadOnlyCollection<Community>> GetPopular()
        {
            return await _context.Communities.ToListAsync();
        }

        public async Task<bool> IsUserSubscribed(ApplicationUser user, Community community)
        {
            return await _context.UserCommunity.AnyAsync(x => x.Community == community && x.User == user);
        }

        public async Task Join(ApplicationUser user, Community community)
        {
            _context.UserCommunity.Add(new UserCommunity() { Community = community, User = user });
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Community>> Search(string search)
        {
            return await _context.Communities
                .Where(x => x.Name.ToLower().Contains(search.ToLower()))
                .ToListAsync();
        }

        public async Task Unsubscribe(ApplicationUser user, Community community)
        {
            var usercomm = await _context.UserCommunity.FirstAsync(x => x.Community == community && x.User == user);
            _context.UserCommunity.Remove(usercomm);
            await _context.SaveChangesAsync();
        }
    }
}
