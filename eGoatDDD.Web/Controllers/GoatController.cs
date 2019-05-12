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
using System.Threading.Tasks;

namespace eGoatDDD.Web.Controllers
{
    public class GoatController : BaseController
    {
        [Route("")]
        [Route("Index")]
        [Route("/Goat/Index/{page?}")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = page ?? 1;



            if (!User.Identity.IsAuthenticated)
            {
                pageSize = 0;
                pageNumber = 0;
            }

            GoatsListNonDtoViewModel goatsLisNonDtotViewModel = goatsLisNonDtotViewModel = await _mediator.Send(new GetAllGoatsQuery(pageNumber, pageSize));

            var doubleTotal = (double)(goatsLisNonDtotViewModel.TotalPages);
            var doublePageSize = (double)(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling(doubleTotal / doublePageSize);
            ViewData["CurrentPage"] = pageNumber;

            return View(goatsLisNonDtotViewModel);
        }

        [Authorize(Policy = "CanEdits")]
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

        [Authorize(Policy = "CanEdits")]
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


        [Authorize(Policy = "CanEdits")]
        public async Task<IActionResult> Edit(long? goatId)
        {
            if (goatId == null)
            {
                return RedirectToAction("Index", "Goat");
            }

            BreedsListViewModel breedsListViewModel = await _mediator.Send(new GetAllBreedsQuery());

            ColorsListViewModel colorsListViewModel = await _mediator.Send(new GetAllColorsQuery());

            GoatsListViewModel goatListForPotentialParentMaternal = await _mediator.Send(new GetAllGoatsPotentialParentQuery(false));

            GoatsListViewModel goatListForPotentialParentSire = await _mediator.Send(new GetAllGoatsPotentialParentQuery(true));

            ViewData["Colors"] = colorsListViewModel.Colors;

            ViewData["Breeds"] = breedsListViewModel.Breeds;

            ViewData["Maternals"] = goatListForPotentialParentMaternal.Goats;

            ViewData["Sires"] = goatListForPotentialParentSire.Goats;

            ViewData["Breeds"] = breedsListViewModel.Breeds;

            GoatNonDtoViewModel goat = await _mediator.Send(new GetGoatQuery(goatId.Value));

            return View(goat);
        }

        [Authorize(Policy = "CanEdits")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]EditGoatCommand command)
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
