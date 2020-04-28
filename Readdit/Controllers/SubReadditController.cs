﻿using System;
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
using Readdit.Infrastructure.Dto;
using Readdit.Models.SubReaddits;

namespace Readdit.Controllers
{
    [Route("sub")]
    public class SubReadditController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubReadditController(IMediator mediator, UserManager<ApplicationUser> um)
        {
            _mediator = mediator;
            _userManager = um;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var popular = await _mediator.Send(new GetPopularSubReaddits());

            var vm = new IndexViewModel
            {
                Popular = popular, SearchResults = new List<SubReadditDto>().AsReadOnly() 
            };

            return View("Index", vm);
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

            request.User = await _userManager.GetUserAsync(HttpContext.User);
            var subId = await _mediator.Send(request);

            return RedirectToAction("GetLinks", new { subReadditName = request.Name, id = subId });
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult>SearchSubReaddits(IFormCollection form)
        {
            var vm = new IndexViewModel
            {
                Popular = await _mediator.Send(new GetPopularSubReaddits()),
                SearchResults = await _mediator.Send(new SearchSubReaddits { SearchString = form["search"] })
            };

            return View(nameof(Index), vm);
        }

        #region Create link for subreaddit
        [HttpGet]
        [Route("{id}/{subReadditName}")]
        public async Task<IActionResult> GetLinks(string subReadditName, int id, int page = 1)
        {
            var links = await _mediator.Send(new GetPagedSubReadditLinks { Page = page, SubReadditName = subReadditName });
            var vm = new SubReadditViewModel
            {
                SubReadditId = id,
                SubReadditName = subReadditName,
                Links = links,
                NextPage = page + 1,
                PreviousPage = page - 1
            };

            return View("SubLinks", vm);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/{subReadditName}/submit-link")]
        public IActionResult CreateLink()
        {
            return View();
        }

        [HttpPost]
        [Route("{id}/{subReadditName}/submit-link")]
        public async Task<ActionResult> CreateLink(CreateLinkCommand request, int id, string subReadditName)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            request.SubReadditId = id;
            request.User = await _userManager.GetUserAsync(HttpContext.User);
            await _mediator.Send(request);

            return RedirectToAction("GetLinks", new { id, subReadditName }); 
        }

        #endregion

        [HttpGet]
        [Route("{id}/{subReadditName}/link/{linkId}")]
        public async Task<IActionResult> LinkDetails(int linkId)
        {
            var res = await _mediator.Send(new GetLinkWithDetails { Id = linkId });
            var vm = new Models.Links.DetailsViewModel { Link = res.link, Comments = res.commests};
            return View("Links/Details", vm);
        }
    }
}