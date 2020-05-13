using System;
using System.Threading.Tasks;
using Moq;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using Readdit.Domain.Services;
using Xunit;

namespace Readdit.Tests
{
    public class VotingServiceUnitTests
    {
        public Mock<ILinksRepository> linksRepositoryMock { get; set; }
        public Mock<IVotesRepository> votesRepositoryMock { get; set; }
        public Mock<IUserRepository> userRepositoryMock { get; set; }
        public IVotingService votingService { get; set; }

        public Link Link { get; set; }
        public Vote Vote { get; set; }
        public ApplicationUser User { get; set; }
        public VotingServiceUnitTests()
        {
            linksRepositoryMock = new Mock<ILinksRepository>();
            votesRepositoryMock = new Mock<IVotesRepository>();
            userRepositoryMock = new Mock<IUserRepository>();

            User = new ApplicationUser {
                Id = Guid.NewGuid().ToString(),
            };

            Link = new Link { Id = 1, Ups = 10, Downs = 5 };

            Vote = new Vote {Link = this.Link, User = this.User, VoteType = VoteType.NonVote };

            linksRepositoryMock
                .Setup(x => x.GetById(Link.Id))
                .Returns(Task.FromResult(Link));

            userRepositoryMock
                .Setup(x => x.GetById(User.Id))
                .Returns(Task.FromResult(User));

            votesRepositoryMock
                .Setup(x => x.GetByLinkId(Link.Id))
                .Returns(Task.FromResult(Vote));

            votesRepositoryMock
                .Setup(x => x.AlreadyVoted(Link.Id, User.Id))
                .Returns(Task.FromResult(true));

            votingService = new VotingService(linksRepositoryMock.Object, votesRepositoryMock.Object, userRepositoryMock.Object);
        }

        [Theory]
        [InlineData(VoteType.Down, VoteType.Down)]
        [InlineData(VoteType.Up, VoteType.NonVote)]
        [InlineData(VoteType.NonVote, VoteType.Down)]
        public async Task DownVote_should_change_vote_type(VoteType currentVoteType, VoteType expected)
        {
            Vote.VoteType = currentVoteType;

            await votingService.DownVote(Link.Id, User.Id);

            Assert.Equal(expected, Vote.VoteType);

        }


        [Fact]
        public async Task UpVote_should_increase_ups_in_link()
        {
            var upsBefore = Link.Ups;

            await votingService.UpVote(Link.Id, User.Id);

            Assert.Equal(upsBefore + 1, Link.Ups);
        }

        [Fact]
        public async Task UpVote_vote_does_not_exists_adding_vote_should_be_invoked()
        {
            votesRepositoryMock
                .Setup(x => x.AlreadyVoted(Link.Id, User.Id))
                .Returns(Task.FromResult(false));

            userRepositoryMock
                .Setup(x => x.GetById(User.Id))
                .Returns(Task.FromResult(User));

            votesRepositoryMock
                .Setup(x => x.Add(new Vote()));

            linksRepositoryMock
                .Setup(x => x.Update(Link));

            var service = new VotingService(linksRepositoryMock.Object,
                votesRepositoryMock.Object, userRepositoryMock.Object);

            
            int upsBefore = Link.Ups;

            await service.UpVote(Link.Id, User.Id);

            Assert.Equal(upsBefore + 1, Link.Ups);
            votesRepositoryMock.Verify(x => x.Add(It.IsAny<Vote>()), Times.Once);


        }
    }
}
