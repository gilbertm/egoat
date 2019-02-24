using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Application.Breeds.Queries;
using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Application.Colors.Queries;
using eGoatDDD.Application.Goats.Commands;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eGoatDDD.Web.Controllers
{
    public class GoatController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            GoatsListViewModel goatsListViewModel = await _mediator.Send(new GetAllGoatsQuery());

            return View(goatsListViewModel);
        }

        public async Task<IActionResult> Create()
        {
            BreedsListViewModel breedsListViewModel = await _mediator.Send(new GetAllBreedsQuery());

            ColorsListViewModel colorsListViewModel = await _mediator.Send(new GetAllColorsQuery());

            ViewData["Colors"] = colorsListViewModel.Colors;

            ViewData["Breeds"] = breedsListViewModel.Breeds;

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
