using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Domain.Models
{
    public class SubReaddit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Link> Links { get; set; }

        public SubReaddit(string name)
        {
            Name = name;
            Links = new List<Link>();
        }
    }
}
