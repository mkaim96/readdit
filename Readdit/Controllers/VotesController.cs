using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Interfaces;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Votes;

namespace Readdit.Controllers
{
    [Route("votes")]
    [Authorize]
    public class VotesController : ControllerBase
    {
        public VotesController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
        }

        [HttpGet]
        [Route("upvote")]
        public async Task<IActionResult> UpVote(int linkId, string returnUrl)
        {
            var request = new UpVoteCommand { LinkId = linkId, UserId = userManager.GetUserId(HttpContext.User) };
            await mediator.Send(request);

            return Redirect(returnUrl);
        }
        [HttpGet]
        [Route("downvote")]
        public async Task<IActionResult> DownVote(int linkId, string returnUrl)
        {
            var request = new DownVoteCommand { LinkId = linkId, UserId = userManager.GetUserId(HttpContext.User) };
            await mediator.Send(request);

            return Redirect(returnUrl);
        }


    }
}