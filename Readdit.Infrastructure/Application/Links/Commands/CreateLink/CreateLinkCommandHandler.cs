using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Links.Commands.CreateLink
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, int>
    {
        private ILinksRepository _linksRepository;

        public CreateLinkCommandHandler(ILinksRepository linksRepo)
        {
            _linksRepository = linksRepo;
        }
        public async Task<int> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            if(!request.Url.StartsWith("http://") || request.Url.StartsWith("https://"))
            {
                request.Url = $"http://{request.Url}";
            }

            var link = new Link(request.Url, request.Description, request.User);

            await _linksRepository.Add(link);

            return link.Id;
        }
    }
}
