using MediatR;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Dto;

namespace Readdit.Infrastructure.Application.Links.Queries.GetLinksByUser
{
    public class GetLinksByUser : IRequest<Paged<LinkDto>>
    {
        public string Username { get; set; }
        public int PageNumber { get; set; }
    }
}