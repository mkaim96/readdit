using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Links.Queries.GetLinksByUser;
using Readdit.Models.Users;

namespace Readdit.Controllers
{

    [Route("users")]
    public class UsersController : ControllerBase
    {
        public UsersController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> Index(string username, int page = 1) 
        {
            var pagedLinks = await mediator.Send(new GetLinksByUser{
                Username = username,
                PageNumber = page
            });

            var vm = new IndexViewModel { PagedLinks = pagedLinks, Username = username };

            return View(vm);
        }
    }
}