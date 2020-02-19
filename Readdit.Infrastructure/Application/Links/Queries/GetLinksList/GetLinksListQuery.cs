using MediatR;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksList
{
    public class GetLinksListQuery : IRequest<ICollection<LinkDto>>
    {
    }
}
