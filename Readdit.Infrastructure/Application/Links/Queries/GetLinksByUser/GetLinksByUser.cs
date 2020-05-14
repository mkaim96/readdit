using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksByUser
{
    public class GetLinksByUser : IRequest<Paged<LinkDto>>
    {
        public string Username { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetLinksByUserHandler : IRequestHandler<GetLinksByUser, Paged<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetLinksByUserHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Paged<LinkDto>> Handle(GetLinksByUser request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;

            var links = await _context.Links
                .Where(x => x.User.UserName == request.Username)
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();

            var totalNumberOfRecords = await _context.Links
                .Where(x => x.User.UserName == request.Username)
                .CountAsync();

            return new Paged<LinkDto>(links, totalNumberOfRecords, request.PageSize, request.PageNumber);
        }
    }
}