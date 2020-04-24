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

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksList
{
    public class GetLinksQueryHandler : IRequestHandler<GetLinksListQuery, IReadOnlyList<LinkDto>>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetLinksQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<LinkDto>> Handle(GetLinksListQuery request, CancellationToken cancellationToken)
        {
            var links = await _context.Links
                .Include(x => x.User)
                .Include(x => x.Comments)
                .Include(x => x.SubReaddit).ThenInclude(x => x.User)
                .Select(x => new LinkDto
                {
                    Id = x.Id,
                    Url = x.Url,
                    Description = x.Description,
                    Ups = x.Ups,
                    Downs = x.Downs,
                    Author = x.User.UserName,
                    CreatedAt = x.CreatedAt,
                    CommentsCount = x.Comments.Count(),
                    SubReaddit = new SubReadditDto 
                    { 
                        Author = x.SubReaddit.User.UserName,
                        Id = x.SubReaddit.Id,
                        Name = x.SubReaddit.Name
                    }
                }).ToListAsync();

            return links.AsReadOnly();
        }
    }
}
