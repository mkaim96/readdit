using MediatR;
using Microsoft.AspNetCore.Identity;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Comments.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private ApplicationDbContext _ctx;

        public CreateCommentCommandHandler(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if(request.User == null)
            {
                throw new ArgumentNullException(nameof(request.User));
            }

            var link = _ctx.Links.First(x => x.Id == request.LinkId);

            var comment = new Comment(request.Content, request.User, link);

            _ctx.Comments.Add(comment);
            await _ctx.SaveChangesAsync();

            return comment.Id;
        }
    }
}
