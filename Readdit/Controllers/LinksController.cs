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
using Readdit.Infrastructure.Application.Links.Commands.DeleteLink;
using Readdit.Infrastructure.Application.Links.Commands.UpdateLink;
using Readdit.Infrastructure.Application.Links.Queries.GetLink;
using Readdit.Infrastructure.Dto;
using Readdit.Models.Links;

namespace Readdit.Controllers
{
    [Authorize]
    [Route("links")]
    public class LinksController : ControllerBase
    {
        public LinksController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
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

            request.User = await userManager.GetUserAsync(HttpContext.User);

            var id = await mediator.Send(request);
            return RedirectToAction(nameof(Details), new { id });
        }
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var res = await mediator.Send(new GetLinkWithDetails() { Id = id });

            var vm = new DetailsViewModel { Link = res.link, Comments = res.commests};

            return View(vm);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var link = await mediator.Send(new GetLinkById {LinkId = id});

            return View("Edit", link);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(LinkDto link) 
        {
            var command = new EditLinkCommand 
            { 
                LinkId = link.Id, Url = link.Url, Description = link.Description 
            };

            await mediator.Send(command);

            return RedirectToAction("Details", new { link.Id });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id, string returnUrl = null)
        {
            await mediator.Send(new DeleteLinkCommand { LinkId = id});

            if(returnUrl != null) {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}