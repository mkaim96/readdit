using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class UserCommunity
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Community Community { get; set; }
    }
}
