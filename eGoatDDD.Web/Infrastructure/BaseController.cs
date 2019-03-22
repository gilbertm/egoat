using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using eGoatDDD.Persistence.Service.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace eGoatDDD.Web.Infrastructure
{
    public abstract class BaseController : Controller
    {
        private IMediator mediator;

        // private IUserService userService;

        private UserManager<ApplicationUser> userManager;

        private eGoatDDDDbContext context;

        protected IMediator _mediator => mediator ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected UserManager<ApplicationUser> _userManager => userManager ?? (userManager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>());

        protected eGoatDDDDbContext _context => context ?? (context = HttpContext.RequestServices.GetService<eGoatDDDDbContext>());

        // protected IUserService _userService => userService ?? (userService = HttpContext.RequestServices.GetService<IUserService>());
    }
}
