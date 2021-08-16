using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Communities.Queries.IsUserSubscribed
{
    public class IsUserSubscribed : IRequest<bool>
    {
        public string CommunityName { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class IsUserSubscribedHandler : IRequestHandler<IsUserSubscribed, bool>
    {
        private readonly ICommunityRepository _communityRepository;

        public IsUserSubscribedHandler(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<bool> Handle(IsUserSubscribed request, CancellationToken cancellationToken)
        {
            var community = await _communityRepository.GetByName(request.CommunityName);
            return await _communityRepository.IsUserSubscribed(request.User, community);
        }
    }
}
