using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddit.Queries.Search
{
    public class SearchCommunities : IRequest<IReadOnlyCollection<CommunityDto>>
    {
        public string SearchString { get; set; }
    }

    public class SearchSubReadditsHandler : IRequestHandler<SearchCommunities, IReadOnlyCollection<CommunityDto>>
    {
        private readonly ICommunityRepository _communityRepository;

        public SearchSubReadditsHandler(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }
        public async Task<IReadOnlyCollection<CommunityDto>> Handle(SearchCommunities request, CancellationToken cancellationToken)
        {
            var communities = await _communityRepository.Search(request.SearchString);
            return communities.Select(x => new CommunityDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList().AsReadOnly();
        }
    }
}
