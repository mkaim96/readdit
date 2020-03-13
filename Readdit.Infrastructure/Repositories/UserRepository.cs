using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> um)
        {
            _userManager = um;
        }
        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.Users.FirstAsync(x => x.Id == id);
        }
    }
}
