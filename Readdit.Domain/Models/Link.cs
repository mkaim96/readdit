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
        public int Ups { get; set; }
        public int Downs { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public SubReaddit? SubReaddit { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public Link(string url, string description, ApplicationUser user, SubReaddit sub = null)
        {
            Url = url;
            Description = description;
            User = user;
            Ups = 0;
            Downs = 0;
            CreatedAt = DateTime.Now;
            Comments = new List<Comment>();
            SubReaddit = sub;
        }
        public Link()
        {

        }
    }
}
