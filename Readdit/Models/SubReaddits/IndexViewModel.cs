using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.SubReaddits
{
    public class IndexViewModel
    {
        public IReadOnlyCollection<SubReadditDto> SearchResults { get; set; }
        public IReadOnlyCollection<SubReadditDto> Popular { get; set; }

    }
}
