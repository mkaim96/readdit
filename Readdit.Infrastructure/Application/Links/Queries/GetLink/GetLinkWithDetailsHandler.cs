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
        private ILinksRepository _linksRepository;
        private IMapper _mapper;

        public GetLinkWithDetailsHandler(ILinksRepository linksRepo, IMapper mapper)
        {
            _linksRepository = linksRepo;
            _mapper = mapper;
        }
        public async Task<(LinkDto link, IReadOnlyList<CommentDto> comments)> Handle(GetLinkWithDetails request,
                CancellationToken cancellationToken)
        {
            var link = await _linksRepository.GetLinkWithCommentsById(request.Id);

            var linkDto = _mapper.Map<Link, LinkDto>(link);
            var commentsDto = _mapper.Map<List<Comment>, List<CommentDto>>(link.Comments.ToList());

            return (linkDto, commentsDto.AsReadOnly());
        }
    }
}
