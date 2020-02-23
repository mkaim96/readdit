using MediatR;
using Microsoft.AspNetCore.Identity;
using Readdit.Domain.Interfaces;
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
        private ILinksRepository _linksRepository;
        private ICommentsRepository _commentsRepository;

        public CreateCommentCommandHandler(ILinksRepository linksRepository, ICommentsRepository commentsRepository)
        {
            _linksRepository = linksRepository;
            _commentsRepository = commentsRepository;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if(request.User == null)
            {
                throw new ArgumentNullException(nameof(request.User));
            }

            var link = await _linksRepository.GetById(request.LinkId);

            var comment = new Comment(request.Content, request.User, link);

            await _commentsRepository.Add(comment);

            return comment.Id;
        }
    }
}
