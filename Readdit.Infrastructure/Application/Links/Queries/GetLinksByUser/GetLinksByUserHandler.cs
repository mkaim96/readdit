using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksByUser
{
    public class GetLinksByUserHandler : IRequestHandler<GetLinksByUser, Paged<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private int _take;
        public GetLinksByUserHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _take = 15;
        }

        public async Task<Paged<LinkDto>> Handle(GetLinksByUser request, CancellationToken cancellationToken)
        {
            var skip = (request.PageNumber - 1) * _take;

            var links = await _context.Links
                .Where(x => x.User.UserName == request.Username)
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .Skip(skip)
                .Take(_take)
                .ToListAsync();

            var totalNumberOfRecords = await _context.Links
                .Where(x => x.User.UserName == request.Username)
                .CountAsync();

            
            var totalNumberOfPages = Math.Ceiling(totalNumberOfRecords / (float)_take);

            return new Paged<LinkDto> 
            {
                PageNumber = request.PageNumber,
                PageSize = _take,
                TotalNumberOfPages = (int)totalNumberOfPages,
                TotalNumberOfRecords = totalNumberOfRecords,
                Items = links
            };
        }
    }
}