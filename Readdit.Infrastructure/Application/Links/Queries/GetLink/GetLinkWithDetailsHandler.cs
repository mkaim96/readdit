using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLink
{
    public class GetLinkWithDetailsHandler : IRequestHandler<GetLinkWithDetails, LinkDto>
    {
        private ILinksRepository _linksRepository;
        private IMapper _mapper;

        public GetLinkWithDetailsHandler(ILinksRepository linksRepo, IMapper mapper)
        {
            _linksRepository = linksRepo;
            _mapper = mapper;
        }
        public async Task<LinkDto> Handle(GetLinkWithDetails request, CancellationToken cancellationToken)
        {
            var link = await _linksRepository.GetLinkWithCommentsById(request.Id);
            return _mapper.Map<LinkDto>(link);
        }
    }
}
