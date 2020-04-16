using MediatR;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.SubReaddit.Queries.GetPopular
{
    public class GetPopularSubReaddits : IRequest<IReadOnlyCollection<SubReadditDto>>
    {

    }

}
