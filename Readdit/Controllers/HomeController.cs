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
    public class HomeController : ControllerBase
    {
        public HomeController(IMediator mediator, UserManager<ApplicationUser> um) : base(mediator, um)
        {
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pagedLinks = await mediator.Send(new GetPagedLinkList { Page = page, PageSize = 2});

            return View(pagedLinks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
