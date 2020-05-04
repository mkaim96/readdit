using MediatR;
using Readdit.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Votes
{
    public class DownVoteCommand : IRequest
    {
        public int LinkId { get; set; }
        public string UserId { get; set; }
    }

    public class DownVoteCommandHandler : IRequestHandler<DownVoteCommand>
    {
        private readonly IVotingService _votingService;

        public DownVoteCommandHandler(IVotingService votingService)
        {
            _votingService = votingService;
        }

        public async Task<Unit> Handle(DownVoteCommand request, CancellationToken cancellationToken)
        {
            await _votingService.DownVote(request.LinkId, request.UserId);
            return new Unit();
        }
    }
}
