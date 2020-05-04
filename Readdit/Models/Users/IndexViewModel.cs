using Readdit.Infrastructure;
using Readdit.Infrastructure.Dto;

namespace Readdit.Models.Users
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public Paged<LinkDto> PagedLinks { get; set; }
    }
}