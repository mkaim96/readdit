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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(IMediator mediator, UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm) : base(mediator, um)
        {
            _signInManager = sm;
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

        public async Task<IActionResult> Logout() 
        {
            if(_signInManager.IsSignedIn(HttpContext.User))
            {
                await _signInManager.SignOutAsync();
            }
            
            return  RedirectToAction("Index", "Home");
        }
    }
}