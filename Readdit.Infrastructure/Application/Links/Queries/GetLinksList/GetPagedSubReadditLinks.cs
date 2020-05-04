﻿using AutoMapper;
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
    public class GetPagedSubReadditLinks : IRequest<Paged<LinkDto>>
    {
        public string SubReadditName { get; set; }
        public int Page { get; set; }
    }

    public class GetSubReadditLinksHandler : IRequestHandler<GetPagedSubReadditLinks, Paged<LinkDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        int _take;

        public GetSubReadditLinksHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _take = 2;
        }
        public async Task<Paged<LinkDto>> Handle(GetPagedSubReadditLinks request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * _take;
            //var links = await _context.Links
            //    .Where(x => x.SubReaddit.Name == request.SubReadditName)
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
                .Where(x => x.SubReaddit.Name == request.SubReadditName)
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Ups - x.Downs)
                .Skip(skip)
                .Take(_take)
                .ToListAsync();

            var totalNumberOfRecords = await _context.Links
                .Where(x => x.SubReaddit.Name == request.SubReadditName)
                .CountAsync();

            var totalNumberOfPages = Math.Ceiling(totalNumberOfRecords / (float)_take);

            return new Paged<LinkDto>
            {
                Items = links,
                PageNumber = request.Page,
                TotalNumberOfPages = (int)totalNumberOfPages,
                TotalNumberOfRecords = totalNumberOfRecords,
                PageSize = _take
            };
        }
    }
}
