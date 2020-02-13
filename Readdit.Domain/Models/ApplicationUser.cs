using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Link> Links { get; set; }
    }
}
