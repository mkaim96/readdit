using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Communities.Commands.JoinCommunity
{
    public class JoinCommunityCommand : IRequest
    {
        public string CommunityName { get; set; }
        public ApplicationUser User { get; set; }
    }


    public class JoinCommunityCommnadHandler : IRequestHandler<JoinCommunityCommand>
    {
        private readonly ICommunityRepository _communityRepository;

        public JoinCommunityCommnadHandler(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<Unit> Handle(JoinCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = await _communityRepository.GetByName(request.CommunityName);
            await _communityRepository.Join(request.User, community);
            return new Unit();
        }
    }
}
