using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Links.Commands.CreateLink;
using Readdit.Infrastructure.Application.Links.Queries.GetLink;
using Readdit.Models.Links;

namespace Readdit.Controllers
{
    [Authorize]
    [Route("links")]
    public class LinksController : Controller
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;

        public LinksController(IMediator mediator, UserManager<ApplicationUser> um)
        {
            _mediator = mediator;
            _userManager = um;
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateLinkCommand request)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", request);
            }

            request.User = await _userManager.GetUserAsync(HttpContext.User);

            var id = await _mediator.Send(request);
            return RedirectToAction(nameof(Details), new { id });
        }
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var res = await _mediator.Send(new GetLinkWithDetails() { Id = id });

            var vm = new DetailsViewModel { Link = res.link, Comments = res.commests};

            return View(vm);
        }
    }
}