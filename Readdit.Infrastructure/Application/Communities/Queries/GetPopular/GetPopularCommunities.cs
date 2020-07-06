using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddit.Queries.GetPopular
{
    public class GetPopularCommunities : IRequest<IReadOnlyCollection<CommunityDto>>
    {

    }

    public class GetPopularSubReadditsHandler : IRequestHandler<GetPopularCommunities, IReadOnlyCollection<CommunityDto>>
    {
        private readonly ICommunityRepository _communityRepository;

        public GetPopularSubReadditsHandler(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<IReadOnlyCollection<CommunityDto>> Handle(GetPopularCommunities request, CancellationToken cancellationToken)
        {
            var communities = await _communityRepository.GetPopular();

            return communities.Select(x => new CommunityDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList().AsReadOnly();
        }
    }

}
