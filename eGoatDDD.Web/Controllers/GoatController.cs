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
            GoatsListNonDtoViewModel goatsLisNonDtotViewModel = await _mediator.Send(new GetAllGoatsQuery());

            return View(goatsLisNonDtotViewModel);
        }

        public async Task<IActionResult> Create()
        {
            BreedsListViewModel breedsListViewModel = await _mediator.Send(new GetAllBreedsQuery());

            ColorsListViewModel colorsListViewModel = await _mediator.Send(new GetAllColorsQuery());

            GoatsListViewModel goatListForPotentialParentMaternal = await _mediator.Send(new GetAllGoatsPotentialParentQuery(false));

            GoatsListViewModel goatListForPotentialParentSire = await _mediator.Send(new GetAllGoatsPotentialParentQuery(true));
 
            ViewData["Colors"] = colorsListViewModel.Colors;

            ViewData["Breeds"] = breedsListViewModel.Breeds;

            ViewData["Maternals"] = goatListForPotentialParentMaternal.Goats;

            ViewData["Sires"] = goatListForPotentialParentSire.Goats;

            ViewData["Breeds"] = breedsListViewModel.Breeds;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateGoatCommand command)
        {
            bool response = false;

            if (ModelState.IsValid)
            {
                response = await _mediator.Send(command);
            }
            
            return RedirectToAction("Index");
        }

    }
}
