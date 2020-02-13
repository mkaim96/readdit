using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
