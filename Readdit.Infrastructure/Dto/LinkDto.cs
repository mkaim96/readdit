using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Dto
{
    public class LinkDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Ups { get; set; }
        public int Downs { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
