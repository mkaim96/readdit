using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksList
{
    public class GetPagedLinkListHandler : IRequestHandler<GetPagedLinkList, IReadOnlyList<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly int _take;

        public GetPagedLinkListHandler(ApplicationDbContext context)
        {
            _context = context;
            _take = 2;
        }


        public async Task<IReadOnlyList<LinkDto>> Handle(GetPagedLinkList request, CancellationToken cancellationToken)
        {
            // Page = 1, Take = 5; skip = 0 * 5 = 0; on page 1 dont skip
            // Page = 2, Take = 5; skip = 1 * 5 = 5; skip first five
            // Page = 3, Take = 5; skip = 2 * 5 = 10; skip first ten, itd
            int skip = (request.Page - 1) * _take;


            var links = await _context.Links
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
