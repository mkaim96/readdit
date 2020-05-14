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
    public class GetPagedLinkList : IRequest<Paged<LinkDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

        public class GetPagedLinkListHandler : IRequestHandler<GetPagedLinkList, Paged<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPagedLinkListHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Paged<LinkDto>> Handle(GetPagedLinkList request, CancellationToken cancellationToken)
        {
            // Page = 1, Take = 5; skip = 0 * 5 = 0; on page 1 dont skip
            // Page = 2, Take = 5; skip = 1 * 5 = 5; skip first five
            // Page = 3, Take = 5; skip = 2 * 5 = 10; skip first ten, itd
            int skip = (request.Page - 1) * request.PageSize;

            //var links = await _context.Links
            //    .Select(x => new LinkDto
            //    {
            //        Id = x.Id,
            //        Url = x.Url,
            //        Description = x.Description,
            //        Ups = x.Ups,
            //        Downs = x.Downs,
            //        Author = x.User.UserName,
            //        CreatedAt = x.CreatedAt,
            //        CommentsCount = x.Comments.Count(),
            //        SubReaddit = new SubReadditDto
            //        {
            //            Author = x.SubReaddit.User.UserName,
            //            Id = x.SubReaddit.Id,
            //            Name = x.SubReaddit.Name
            //        }
            //    })
            //    .OrderByDescending(x => x.Ups - x.Downs)
            //    .Skip(skip)
            //    .Take(_take)
            //    .ToListAsync();

            // using automapper
            var links = await _context.Links
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Ups - x.Downs)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();

            var totalNumberOfRecords = await _context.Links.CountAsync();

            return new Paged<LinkDto>(links,
                totalNumberOfRecords,
                request.PageSize,
                request.Page);
        }
    }
}
