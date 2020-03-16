using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readdit.Domain.Services
{
    public class VotingService : IVotingService
    {
        private readonly ILinksRepository _linksRepository;
        private readonly IVotesRepository _votesRepository;
        private readonly IUserRepository _userRepository;

        public VotingService(
            ILinksRepository linksRepository,
            IVotesRepository votesRepository,
            IUserRepository userRepository)
        {
            _linksRepository = linksRepository;
            _votesRepository = votesRepository;
            _userRepository = userRepository;
        }
        public async Task DownVote(int linkId, string userId)
        {
            var link = await _linksRepository.GetById(linkId);

            if(await _votesRepository.AlreadyVoted(linkId, userId))
            {
                var vote = await _votesRepository.GetByLinkId(linkId);
                switch (vote.VoteType)
                {
                    // Down - return
                    case VoteType.Down: return;
                    // Up - none vote
                    case VoteType.Up:
                        {
                            vote.VoteType = VoteType.NonVote;
                            link.Ups--;
                            break;
                        }
                    // none vote - down
                    case VoteType.NonVote:
                        {
                            vote.VoteType = VoteType.Down;
                            link.Downs++;
                            break;
                        }
                }
                // TODO: Save vote and link to database
            } 
            else
            {
                // create new down vote
                var user = await _userRepository.GetById(userId);
                var newVote = new Vote(link, user, VoteType.Down);
                link.Downs++;
                await _votesRepository.Add(newVote);

            }
        }

        public async Task UpVote(int linkId, string userId)
        {
            var link = await _linksRepository.GetById(linkId);

            if (await _votesRepository.AlreadyVoted(linkId, userId))
            {
                var vote = await _votesRepository.GetByLinkId(linkId);
                switch (vote.VoteType)
                {
                    case VoteType.Up: return;
                    case VoteType.Down:
                        {
                            link.Downs--;
                            vote.VoteType = VoteType.NonVote;
                            break;
                        }
                    case VoteType.NonVote:
                        {
                            link.Ups++;
                            vote.VoteType = VoteType.Up;
                            break;
                        }
                }
            }
            else
            {
                // create new up vote
                var user = await _userRepository.GetById(userId);
                var newVote = new Vote(link, user, VoteType.Up);
                link.Ups++;
                await _votesRepository.Add(newVote);
            }
        }
    }
}
