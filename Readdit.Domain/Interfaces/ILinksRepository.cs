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
        Task<Link> GetById(int id);
        Task Update(Link link);
        Task Delete(Link link);
    }
}
