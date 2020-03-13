using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetById(string id);
    }
}
