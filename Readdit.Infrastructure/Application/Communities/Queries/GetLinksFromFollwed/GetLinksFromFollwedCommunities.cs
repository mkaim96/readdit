using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Communities.Queries.GetLinksFromFollwed
{
    public class GetLinksFromFollwedCommunities : IRequest<Paged<LinkDto>>
    {
        public ApplicationUser User { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetLinksFromFollowedCommunitiesHandler : IRequestHandler<GetLinksFromFollwedCommunities, Paged<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLinksFromFollowedCommunitiesHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Paged<LinkDto>> Handle(GetLinksFromFollwedCommunities request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;

            var followed = await _context.UserCommunity
                .Where(x => x.User == request.User)
                .Select(x => x.Community.Name)
                .ToListAsync();

            var links = await _context.Links
                .Where(x => followed.Contains(x.Community.Name))
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Ups - x.Downs)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();

            var totalNumberOfRecords = await _context.Links
                .Where(x => x.User == request.User)
                .CountAsync();

            return new Paged<LinkDto>(links, totalNumberOfRecords, request.PageSize, request.PageNumber);
        }
    }
}
