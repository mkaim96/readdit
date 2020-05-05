using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readdit.Domain.Interfaces;

namespace Readdit.Infrastructure.Application.Links.Commands.DeleteLink
{
    public class DeleteLinkCommand : IRequest
    {
        public int LinkId { get; set; }
    }

    public class DeleteLinkCommandHandler : IRequestHandler<DeleteLinkCommand, Unit>
    {
        private readonly ILinksRepository _linksRepository;

        public DeleteLinkCommandHandler(ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task<Unit> Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
        {
            var link = await _linksRepository.GetById(request.LinkId);
            await _linksRepository.Delete(link);

            return new Unit();
        }
    }
}