using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> Add(Comment comment)
        {
            await _context.AddAsync(comment);
            return comment;
        }
    }
}
