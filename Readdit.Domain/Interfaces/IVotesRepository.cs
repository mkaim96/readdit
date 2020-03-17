using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface IVotesRepository
    {
        Task<bool> AlreadyVoted(int linkId, string userId);
        Task<Vote> GetByLinkId(int linkId);
        Task Add(Vote vote);
        Task Update(Vote vote);
    }
}
