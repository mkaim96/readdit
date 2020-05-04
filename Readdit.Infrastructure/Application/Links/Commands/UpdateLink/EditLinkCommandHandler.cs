using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readdit.Domain.Interfaces;

namespace Readdit.Infrastructure.Application.Links.Commands.UpdateLink
{
    public class EditLinkCommandHandler : IRequestHandler<EditLinkCommand>
    {
        private readonly ILinksRepository _linksRepository;

        public EditLinkCommandHandler(ILinksRepository linksService)
        {
            _linksRepository = linksService;
        }

        public async Task<Unit> Handle(EditLinkCommand request, CancellationToken cancellationToken)
        {
            var link = await _linksRepository.GetById(request.LinkId);

            link.UpdatedAt = DateTime.Now;
            link.Description = request.Description;
            link.Url = request.Url;

            await _linksRepository.Update(link);
            return new Unit();
        }
    }
}