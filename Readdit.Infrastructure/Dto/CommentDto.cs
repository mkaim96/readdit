using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
