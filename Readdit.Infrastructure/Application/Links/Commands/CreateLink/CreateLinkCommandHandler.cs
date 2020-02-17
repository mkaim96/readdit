using MediatR;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Readdit.Infrastructure.Application.Links.Commands.CreateLink
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, int>
    {
        private ApplicationDbContext _ctx;

        public CreateLinkCommandHandler(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<int> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var link = new Link(request.Url, request.Description);
            link.User = request.User;

            await _ctx.Links.AddAsync(link);
            _ctx.SaveChanges();

            return link.Id;
        }
    }
}
