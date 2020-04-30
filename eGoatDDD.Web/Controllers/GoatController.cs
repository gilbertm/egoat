﻿using eGoatDDD.Application.Breeds.Models;
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
        [Route("/Goat/{page?}")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page ?? 1;

            if (!User.Identity.IsAuthenticated)
            {
                pageSize = 40;
            }

            GoatsListNonDtoViewModel goatsLisNonDtotViewModel = await _mediator.Send(new GetAllGoatsQuery
            {
                PageNumber = pageNumber <= 0 ? 1 : pageNumber,
                Filter = "alive",
                PageSize = pageSize
            });

            var doubleTotal = (double)(goatsLisNonDtotViewModel.TotalPages);
            var doublePageSize = (double)(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling(doubleTotal / doublePageSize);
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["Listing"] = "alive";

            return View(goatsLisNonDtotViewModel);
        }

        [Route("/Goat/Listing/{type?}/{page?}")]
        [Authorize(Policy = "CanEdits")]
        public async Task<IActionResult> Listing(string listingType, int? page)
        {
            int pageSize = 75;
            int pageNumber = page ?? 1;

            string type = string.IsNullOrWhiteSpace(listingType) ? "thumb" : listingType; // thumb - default, 2 row - tabular

            if (!User.Identity.IsAuthenticated)
            {
                pageSize = 150;
            }

            GoatsListNonDtoViewModel goatsLisNonDtotViewModel = await _mediator.Send(new GetAllGoatsQuery
            {
                PageNumber = pageNumber <= 0 ? 1 : pageNumber,
                Filter = "alive",
                PageSize = pageSize
            });

            var doubleTotal = (double)(goatsLisNonDtotViewModel.TotalPages);
            var doublePageSize = (double)(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling(doubleTotal / doublePageSize);
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["Listing"] = "alive";

            return View(goatsLisNonDtotViewModel);
        }


        [Route("/Goat/Filtered/{filter}/{page?}")]
        [Authorize(Policy = "CanEdits")]
        public async Task<IActionResult> Filter(string filter, int? page)
        {
            int pageSize = 30;
            int pageNumber = page ?? 1;

            if (!User.Identity.IsAuthenticated)
            {
                pageSize = 40;
            }

            GoatsListNonDtoViewModel goatsLisNonDtotViewModel = await _mediator.Send(new GetAllGoatsQuery {
                PageNumber = pageNumber <= 0 ? 1 : pageNumber,
                Filter = filter,
                PageSize = pageSize
            });

            var doubleTotal = (double)(goatsLisNonDtotViewModel.TotalPages);
            var doublePageSize = (double)(pageSize);

            ViewData["TotalPages"] = (int)Math.Ceiling(doubleTotal / doublePageSize);
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["Listing"] = filter;

            return View("Index", goatsLisNonDtotViewModel);
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

        [Route("/Goat/Get/{goatId?}")]
        public async Task<IActionResult> Get(long? goatId)
        {
            if (goatId == null)
            {
                return RedirectToAction("Index", "Goat");
            }

            GoatViewModel goat = await _mediator.Send(new GetGoatQuery(goatId.Value));

            return View("View",goat);
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

            GoatViewModel goat = await _mediator.Send(new GetGoatQuery(goatId.Value));

            return View(goat);
        }

        [Authorize(Policy = "CanEdits")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]EditGoatCommand command)
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
            }

            return RedirectToAction("Get", new { goatId = command.Id });
        }

        [Authorize(Policy = "CanEdits")]
        [Route("goat/restore/{id}")]
        public async Task<IActionResult> Restore(long Id)
        {
            RestoreGoatCommand restoreGoatCommand = new RestoreGoatCommand
            {
                Id = Id,
            };

            var response = await _mediator.Send(restoreGoatCommand);


            return RedirectToAction("Index");

        }
    }
}
