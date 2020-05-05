using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Ef;

namespace Readdit.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly IMediator mediator;
        protected readonly UserManager<ApplicationUser> userManager;

        public ControllerBase(IMediator mediator, UserManager<ApplicationUser> um)
        {
            this.mediator = mediator;
            this.userManager = um;
        }
    }
}