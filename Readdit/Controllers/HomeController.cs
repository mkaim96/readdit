using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Application.Links.Queries.GetLinksList;
using Readdit.Infrastructure.Dto;
using Readdit.Models;
using Readdit.Models.Links;

namespace Readdit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var links = await _mediator.Send(new GetPagedLinkList { Page = page});

            var vm = new IndexViewModel { Links = links, NextPage = page + 1 };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
