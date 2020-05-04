using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLink
{
    public class GetLinkById : IRequest<LinkDto>
    {
        public int LinkId { get; set; }
    }

    public class GetLinkByIdHandler : IRequestHandler<GetLinkById, LinkDto>
    {
        private readonly ILinksRepository _linksRepository;
        public readonly IMapper _mapper;

        public GetLinkByIdHandler(ILinksRepository linksRepository, IMapper mapper)
        {
            _linksRepository = linksRepository;
            _mapper = mapper;
        }

        public async Task<LinkDto> Handle(GetLinkById request, CancellationToken cancellationToken)
        {
            var link = await _linksRepository.GetById(request.LinkId);
            return _mapper.Map<LinkDto>(link);
        }
    }
}