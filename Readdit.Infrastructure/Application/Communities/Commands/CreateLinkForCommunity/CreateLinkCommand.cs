﻿using MediatR;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.SubReaddits.Commands.CreateLinkForSubReaddit
{
    public class CreateLinkCommand : IRequest<int>
    {
        public ApplicationUser User { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string CommunityName { get; set; }
    }

    public class CreateLinkHandler : IRequestHandler<CreateLinkCommand, int>
    {
        private ILinksRepository _linksRepository;
        private ICommunityRepository _communityRepository;

        public CreateLinkHandler(ILinksRepository linksRepository, ICommunityRepository communityRepository)
        {
            _linksRepository = linksRepository;
            _communityRepository = communityRepository;
        }
        public async Task<int> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.Url.StartsWith("http://") && !request.Url.StartsWith("https://"))
            {
                request.Url = $"http://{request.Url}";
            }

            var subreaddit = await _communityRepository.GetByName(request.CommunityName);
            var link = new Link(request.Url, request.Description, request.User, subreaddit);

            await _linksRepository.Add(link);

            return link.Id;
        }
    }

}
