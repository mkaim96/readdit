using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Links.Queries.GetLinksList;
using Readdit.Infrastructure.Application.SubReaddit.Queries.GetPopular;
using Readdit.Infrastructure.Application.SubReaddit.Queries.Search;
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
        public async Task<IActionResult> Index(string search = "")
        {
            var searched = new List<SubReadditDto>();
            if(!(search.Length == 0))
            {
                var x = await _mediator.Send(new SearchSubReaddits { SearchString = search });
                searched = x.ToList();
            }
            var popular = await _mediator.Send(new GetPopularSubReaddits());

            var vm = new IndexViewModel { Popular = popular, SearchResults = searched.AsReadOnly() };

            return View("Index", vm);
        }

        [HttpGet]
        [Route("{subReadditName}")]
        public async Task<IActionResult> GetLinks(string subReadditName, int page = 1)
        {
            var links = await _mediator.Send(new GetPagedSubReadditLinks { Page = page, SubReadditName = subReadditName });
            var vm = new SubLinksViewModel
            {
                SubReadditName = subReadditName,
                Links = links,
                NextPage = page + 1,
                PreviousPage = page - 1
            };

            return View("SubLinks", vm);
        }

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
            request.User = await _userManager.GetUserAsync(HttpContext.User);
            var subId = await _mediator.Send(request);

            return View("SubReaddit", subId);
        }
    }
}