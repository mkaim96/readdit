using Readdit.Infrastructure;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.SubReaddits
{
    public class CommunityViewModel
    {
        public Paged<LinkDto> PagedLinks { get; set; }
        public CommunityDto Community { get; set; }
    }
}
