using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Interfaces
{
    public interface ICommentsRepository
    {
        Task<Comment> Add(Comment comment);
    }
}
