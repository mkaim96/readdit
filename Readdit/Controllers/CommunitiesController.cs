using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Links.Queries.GetLink;
using Readdit.Infrastructure.Application.Links.Queries.GetLinksList;
using Readdit.Infrastructure.Application.SubReaddit.Queries.GetPopular;
using Readdit.Infrastructure.Application.SubReaddit.Queries.Search;
using Readdit.Infrastructure.Application.SubReaddits.Commands.CreateLinkForSubReaddit;
using Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit;
using Readdit.Infrastructure.Application.SubReaddits.Queries.GetByName;
using Readdit.Infrastructure.Dto;
using Readdit.Models.SubReaddits;

namespace Readdit.Controllers
{
    [Route("c")]
    public class CommunitiesController : ControllerBase
    {
        public CommunitiesController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var popular = await mediator.Send(new GetPopularCommunities());

            var vm = new IndexViewModel
            {
                Popular = popular, SearchResults = new List<CommunityDto>().AsReadOnly() 
            };

            return View("Index", vm);
        }

        [HttpGet]
        [Route("{subReadditName}/link/{linkId}")]
        public async Task<IActionResult> LinkDetails(int linkId)
        {
            var res = await mediator.Send(new GetLinkWithDetails { Id = linkId });
            var vm = new Models.Links.DetailsViewModel { Link = res.link, Comments = res.commests};
            return View("Links/Details", vm);
        }

        [HttpGet]
        [Route("{communityName}")]
        public async Task<IActionResult> GetLinks(string communityName, int page = 1)
        {
            var pagedLinks = await mediator.Send(new GetPagedCommunityLinks { Page = page, CommunityName = communityName, PageSize = 2 });
            var community = await mediator.Send(new GetCommunityByName { Name = communityName });
            var vm = new CommunityViewModel
            {
                Community = community,
                PagedLinks = pagedLinks
            };

            return View("CommunityLinks", vm);
        }

        [HttpPost]
        public async Task<IActionResult>SearchCommunities(IFormCollection form)
        {
            var vm = new IndexViewModel
            {
                Popular = await mediator.Send(new GetPopularCommunities()),
                SearchResults = await mediator.Send(new SearchCommunities { SearchString = form["search"] })
            };

            return View(nameof(Index), vm);
        }

        #region Create subreaddit
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateCommunityCommand request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            request.User = await userManager.GetUserAsync(HttpContext.User);
            await mediator.Send(request);

            return RedirectToAction("GetLinks", new { communityName = request.Name });
        }
        #endregion

        #region Create link for subreaddit

        [Authorize]
        [HttpGet]
        [Route("{communityName}/submit-link")]
        public IActionResult CreateLink(string communityName)
        {
            var model = new CreateLinkCommand { CommunityName = communityName };
            return View(model);
        }

        [HttpPost]
        [Route("{communityName}/submit-link")]
        public async Task<ActionResult> CreateLink(CreateLinkCommand request, string communityName)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            request.CommunityName = communityName;
            request.User = await userManager.GetUserAsync(HttpContext.User);
            await mediator.Send(request);

            return RedirectToAction("GetLinks", new { communityName }); 
        }

        #endregion
    }
}