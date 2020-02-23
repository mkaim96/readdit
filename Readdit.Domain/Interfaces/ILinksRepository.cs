using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface ILinksRepository
    {
        Task<Link> Add(Link link);
        Task<IReadOnlyList<Link>> GetAll();
        Task<Link> GetLinkWithCommentsById(int id);
        Task<Link> GetById(int id);
    }
}
