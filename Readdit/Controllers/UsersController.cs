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
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IMediator mediator, UserManager<ApplicationUser> um)
        {
            _mediator = mediator;
            _userManager = um;
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> Index(string username, int page = 1) 
        {
            var pagedLinks = await _mediator.Send(new GetLinksByUser{
                Username = username,
                PageNumber = page
            });

            var vm = new IndexViewModel { PagedLinks = pagedLinks, Username = username };

            return View(vm);
        }
    }
}