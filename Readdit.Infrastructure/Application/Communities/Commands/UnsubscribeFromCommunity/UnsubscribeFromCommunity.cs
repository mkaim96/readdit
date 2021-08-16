using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Communities.Commands.UnsubscribeFromCommunity
{
    public class UnsubscribeFromCommunity : IRequest
    {
        public string CommunityName { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class UnsubscribeFromCommunityHandler : IRequestHandler<UnsubscribeFromCommunity>
    {
        private readonly ICommunityRepository _communityRepository;

        public UnsubscribeFromCommunityHandler(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<Unit> Handle(UnsubscribeFromCommunity request, CancellationToken cancellationToken)
        {
            var community = await _communityRepository.GetByName(request.CommunityName);
            await _communityRepository.Unsubscribe(request.User, community);
            return new Unit();
        }
    }
}
