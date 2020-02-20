using MediatR;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.Comments.Commands
{
    public class CreateCommentCommand : IRequest<int>
    {
        public ApplicationUser User { get; set; }
        public string Content { get; set; }
        public int LinkId { get; set; }
    }
}
