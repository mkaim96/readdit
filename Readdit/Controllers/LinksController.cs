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

namespace Readdit.Controllers
{
    [Authorize]
    public class LinksController : Controller
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;

        public LinksController(IMediator mediator, UserManager<ApplicationUser> um)
        {
            _mediator = mediator;
            _userManager = um;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLinkCommand request)
        {
            request.User = await _userManager.GetUserAsync(HttpContext.User);

            var id = await _mediator.Send(request);

            return Ok();
        }
    }
}