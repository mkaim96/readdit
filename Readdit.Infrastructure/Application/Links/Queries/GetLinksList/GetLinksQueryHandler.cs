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
        private ILinksRepository _linksRepository;
        private IMapper _mapper;

        public GetLinksQueryHandler(ApplicationDbContext context, ILinksRepository linksRepo, IMapper mapper)
        {
            _context = context;
            _linksRepository = linksRepo;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<LinkDto>> Handle(GetLinksListQuery request, CancellationToken cancellationToken)
        {
            var links = await _context.Links
                .Include(x => x.User)
                .Include(x => x.Comments)
                .Select(x => new LinkDto
                {
                    Id = x.Id,
                    Url = x.Url,
                    Description = x.Description,
                    Ups = x.Ups,
                    Downs = x.Downs,
                    Author = x.User.UserName,
                    CreatedAt = x.CreatedAt,
                    CommentsCount = x.Comments.Count()
                }).ToListAsync();

            return links.AsReadOnly();
        }
    }
}
