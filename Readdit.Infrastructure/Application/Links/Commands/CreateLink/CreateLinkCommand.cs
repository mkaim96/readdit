using MediatR;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.Links.Commands.CreateLink
{
    public class CreateLinkCommand : IRequest<int>
    {
        public ApplicationUser User { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

    }
}
