using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
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
    public class GetLinkWithDetailsHandler : IRequestHandler<GetLinkWithDetails, (LinkDto link, IReadOnlyList<CommentDto> comments)>
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public GetLinkWithDetailsHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<(LinkDto link, IReadOnlyList<CommentDto> comments)> Handle(GetLinkWithDetails request,
                CancellationToken cancellationToken)
        {
            var linkDto = await _context.Links
                .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
                .FirstAsync(x => x.Id == request.Id);

            var commentsDto = await _context.Comments
                .Where(x => x.Link.Id == request.Id)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
               

            return (linkDto, commentsDto.AsReadOnly());
        }
    }
}
