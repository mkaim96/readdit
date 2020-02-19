using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksList
{
    public class GetLinksQueryHandler : IRequestHandler<GetLinksListQuery, ICollection<LinkDto>>
    {
        private ApplicationDbContext _ctx;
        private IMapper _mapper;

        public GetLinksQueryHandler(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<ICollection<LinkDto>> Handle(GetLinksListQuery request, CancellationToken cancellationToken)
        {
            var links = await _ctx.Links
                .Include(x => x.User)
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return links;
        }
    }
}
