using Readdit.Infrastructure.Application.Comments.Commands;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.Links
{
    public class DetailsViewModel
    {
        public LinkDto Link { get; set; }
        public IReadOnlyList<CommentDto> Comments { get; set; }
        public CreateCommentCommand CreateComment { get; set; }
    }
}
