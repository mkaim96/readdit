using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.SubReaddits
{
    public class IndexViewModel
    {
        public IReadOnlyCollection<CommunityDto> SearchResults { get; set; }
        public IReadOnlyCollection<CommunityDto> Popular { get; set; }

    }
}
