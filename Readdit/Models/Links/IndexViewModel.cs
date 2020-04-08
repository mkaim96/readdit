using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.Links
{
    public class IndexViewModel
    {
        public IReadOnlyList<LinkDto> Links { get; set; }
        public int NextPage { get; set; }
    }
}
