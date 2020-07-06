using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Dto
{
    public class CommunityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
