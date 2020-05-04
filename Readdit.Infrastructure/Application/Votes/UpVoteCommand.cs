using MediatR;
using Readdit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Votes
{
    public class UpVoteCommand : IRequest
    {
        public int LinkId { get; set; }
        public string UserId { get; set; }
    }

    public class UpVoteCommandHandler : IRequestHandler<UpVoteCommand>
    {
        private IVotingService _votingService;

        public UpVoteCommandHandler(IVotingService votingService)
        {
            _votingService = votingService;
        }

        public async Task<Unit> Handle(UpVoteCommand request, CancellationToken cancellationToken)
        {
            await _votingService.UpVote(request.LinkId, request.UserId);
            return new Unit();
        }
    }
}
