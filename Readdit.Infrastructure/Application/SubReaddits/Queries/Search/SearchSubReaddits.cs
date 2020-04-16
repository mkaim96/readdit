using MediatR;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.SubReaddit.Queries.Search
{
    public class SearchSubReaddits : IRequest<IReadOnlyCollection<SubReadditDto>>
    {
        public string SearchString { get; set; }
    }
}
