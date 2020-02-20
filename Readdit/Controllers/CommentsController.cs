using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Comments.Commands;

namespace Readdit.Controllers
{
    [Route("comments")]
    public class CommentsController : Controller
    {
        private IMediator _mediator;
        private UserManager<ApplicationUser> _userManager;

        public CommentsController(IMediator mediator, UserManager<ApplicationUser> um)
        {
            _mediator = mediator;
            _userManager = um;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateCommentCommand request)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Links", new { id = request.LinkId });
            }

            request.User = await _userManager.GetUserAsync(HttpContext.User);

            await _mediator.Send(request);

            return RedirectToAction("Details", "Links", new { id = request.LinkId });
        }
    }
}