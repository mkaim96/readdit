using MediatR;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLink
{
    public class GetLinkById : IRequest<LinkDto>
    {
        public int Id { get; set; }
    }
}
