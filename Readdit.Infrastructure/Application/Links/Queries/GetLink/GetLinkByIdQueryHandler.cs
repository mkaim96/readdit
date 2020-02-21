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

namespace Readdit.Infrastructure.Application.Links.Queries.GetLink
{
    public class GetLinkByIdQueryHandler : IRequestHandler<GetLinkById, LinkDto>
    {
        private ApplicationDbContext _ctx;
        private IMapper _mapper;

        public GetLinkByIdQueryHandler(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<LinkDto> Handle(GetLinkById request, CancellationToken cancellationToken)
        {
            return await _ctx.Links
                .Include(x => x.Comments)
                .Include(x => x.User)
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .FirstAsync(x => x.Id == request.Id);
        }
    }
}
