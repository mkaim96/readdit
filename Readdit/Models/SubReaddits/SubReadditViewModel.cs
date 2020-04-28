﻿using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readdit.Models.SubReaddits
{
    public class SubReadditViewModel
    {
        public IReadOnlyList<LinkDto> Links { get; set; }
        public SubReadditDto SubReaddit { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
    }
}
