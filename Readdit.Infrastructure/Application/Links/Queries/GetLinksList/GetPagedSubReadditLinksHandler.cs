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
    public class GetSubReadditLinksHandler : IRequestHandler<GetPagedSubReadditLinks, IReadOnlyList<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        int _take;

        public GetSubReadditLinksHandler(ApplicationDbContext context)
        {
            _context = context;
            _take = 2;
        }
        public async Task<IReadOnlyList<LinkDto>> Handle(GetPagedSubReadditLinks request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * _take;
            var links = await _context.Links
                .Where(x => x.SubReaddit.Name == request.SubReadditName)
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
                })
                .OrderByDescending(x => x.Ups - x.Downs)
                .Skip(skip)
                .Take(_take)
                .ToListAsync();

            return links.AsReadOnly();
        }
    }
}
