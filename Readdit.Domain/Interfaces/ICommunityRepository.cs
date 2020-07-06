using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface ICommunityRepository
    {
        Task<IReadOnlyCollection<Community>> Search(string search);
        Task<IReadOnlyCollection<Community>> GetPopular();
        Task<Community> Add(Community community);
        Task<Community> GetByName(string name);
        Task<bool> Exists(string name);
    }
}
