using eGoatDDD.Application.Goats.Commands;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.WebMVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Controllers
{
    public class GoatController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            GoatsListViewModel goatsListViewModel = await _mediator.Send(new GetAllGoatsQuery());

            return View(goatsListViewModel);
        }

        public async Task<IActionResult> Create()
        {   GoatsListViewModel goatsListViewModel = await _mediator.Send(new GetAllGoatsQuery());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateGoatCommand command)
        {
            if (ModelState.IsValid)
            {
                GoatViewModel goatViewModel = await _mediator.Send(command);
            }

            GoatsListViewModel goatsListViewModel = await _mediator.Send(new GetAllGoatsQuery());

            return View();
        }

    }
}
