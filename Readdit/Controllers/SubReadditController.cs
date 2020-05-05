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
    [Route("sub")]
    public class SubReadditController : ControllerBase
    {
        public SubReadditController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var popular = await mediator.Send(new GetPopularSubReaddits());

            var vm = new IndexViewModel
            {
                Popular = popular, SearchResults = new List<SubReadditDto>().AsReadOnly() 
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
        [Route("{subReadditName}")]
        public async Task<IActionResult> GetLinks(string subReadditName, int page = 1)
        {
            var pagedLinks = await mediator.Send(new GetPagedSubReadditLinks { Page = page, SubReadditName = subReadditName });
            var subreaddit = await mediator.Send(new GetSubReadditByName { Name = subReadditName });
            var vm = new SubReadditViewModel
            {
                SubReaddit = subreaddit,
                PagedLinks = pagedLinks
            };

            return View("SubReadditLinks", vm);
        }

        [HttpPost]
        public async Task<IActionResult>SearchSubReaddits(IFormCollection form)
        {
            var vm = new IndexViewModel
            {
                Popular = await mediator.Send(new GetPopularSubReaddits()),
                SearchResults = await mediator.Send(new SearchSubReaddits { SearchString = form["search"] })
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
        public async Task<IActionResult> Create(CreateSubReadditCommand request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            request.User = await userManager.GetUserAsync(HttpContext.User);
            await mediator.Send(request);

            return RedirectToAction("GetLinks", new { subReadditName = request.Name });
        }
        #endregion

        #region Create link for subreaddit

        [Authorize]
        [HttpGet]
        [Route("{subReadditName}/submit-link")]
        public IActionResult CreateLink()
        {
            return View();
        }

        [HttpPost]
        [Route("{subReadditName}/submit-link")]
        public async Task<ActionResult> CreateLink(CreateLinkCommand request, string subReadditName)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            request.SubReadditName = subReadditName;
            request.User = await userManager.GetUserAsync(HttpContext.User);
            await mediator.Send(request);

            return RedirectToAction("GetLinks", new { subReadditName }); 
        }

        #endregion
    }
}