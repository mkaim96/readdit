using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Readdit.Infrastructure.Application.Links.Queries.GetLinksList;
using Readdit.Infrastructure.Application.SubReaddit.Commands.CreateSubReaddit;

namespace Readdit.Controllers
{
    [Route("sub")]
    public class SubReadditController : Controller
    {
        private readonly IMediator _mediator;

        public SubReadditController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("{subReadditName}")]
        public async Task<IActionResult> GetSubReadditLinks(string subReadditName, int page = 1)
        {
            var links = await _mediator.Send(new GetPagedSubReadditLinks { Page = page, SubReadditName = subReadditName });
            return View("Index", links);
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
            var subId = await _mediator.Send(request);
            return View("SubReaddit", subId);
        }
    }
}