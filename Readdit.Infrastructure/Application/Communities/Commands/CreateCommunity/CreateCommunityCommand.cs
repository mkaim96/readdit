using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit
{
    public class CreateCommunityCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class CreateSubReadditHandler : IRequestHandler<CreateCommunityCommand, int>
    {
        private readonly ICommunityRepository _communityRepository;

        public CreateSubReadditHandler(ICommunityRepository subReadditRepository)
        {
            _communityRepository = subReadditRepository;
        }

        public async Task<int> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = new Community(request.Name, request.User, request.Description);
            await _communityRepository.Add(community);

            return community.Id;
        }
    }
}
