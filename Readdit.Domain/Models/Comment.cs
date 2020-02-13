using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Link Link { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
