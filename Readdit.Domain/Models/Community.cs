using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class Community
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Link> Links { get; set; }
        public IEnumerable<UserCommunity> UserCommunity { get; set; }
        public ApplicationUser User { get; set; }
        public string  Description { get; set; }

        public Community(string name, ApplicationUser user, string description)
        {
            Name = name;
            User = user;
            Links = new List<Link>();
            Description = description;
        }

        private Community()
        {
        }
    }
}
