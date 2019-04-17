﻿using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Application.Services.Models;
using eGoatDDD.Application.Services.Queries;
using System;
using eGoatDDD.Application.Goats.Commands;

namespace eGoatDDD.Web.Controllers
{
    public class ApiDisposalController : BaseController
    {
        [Route("api/disposal/goat")]
        public async Task<JsonResult> Put(long GoatId, string Type, string Reason, DateTime DisposedOn)
        {
            DeleteGoatCommand deleteGoatCommand = new DeleteGoatCommand
            {
                Id = GoatId,
                Reason = Reason,
                DisposedOn = DisposedOn
            };

            var response = await _mediator.Send(deleteGoatCommand);


            if (response)
            {
                return Json(new { error = 0, message = "Disposal Successful." });
            }

            return Json(new { error = 1, response = "No service." });

        }

    }

}
