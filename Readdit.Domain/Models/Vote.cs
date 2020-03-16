using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public Link Link { get; set; }
        public ApplicationUser User { get; set; }
        public VoteType VoteType { get; set; }

        public Vote(Link link, ApplicationUser user, VoteType voteType)
        {
            Link = link;
            User = user;
            VoteType = voteType;
        }
        public Vote()
        {

        }
    }
}
