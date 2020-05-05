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
    public class CommentsController : ControllerBase
    {
        public CommentsController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateCommentCommand request)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Links", new { id = request.LinkId });
            }

            request.User = await userManager.GetUserAsync(HttpContext.User);

            await mediator.Send(request);

            return RedirectToAction("Details", "Links", new { id = request.LinkId });
        }
    }
}