using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.Votes
{
    public class UpVoteCommand : IRequest
    {
        public int LinkId { get; set; }
        public string UserId { get; set; }
    }
}
