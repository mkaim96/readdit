using MediatR;
using Readdit.Infrastructure.Dto;

namespace Readdit.Infrastructure.Application.Links.Commands.UpdateLink
{
    public class EditLinkCommand : IRequest
    {
        public int LinkId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

    }
}