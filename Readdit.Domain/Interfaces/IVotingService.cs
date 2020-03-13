using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface IVotingService
    {
        Task UpVote(int linkId, string userId);
        Task DownVote(int linkId, string userId);
    }
}
