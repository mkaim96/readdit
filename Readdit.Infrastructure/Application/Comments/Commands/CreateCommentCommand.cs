using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Comments.Commands
{
    public class CreateCommentCommand : IRequest<int>
    {
        public ApplicationUser User { get; set; }
        public string Content { get; set; }
        public int LinkId { get; set; }
    }

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
