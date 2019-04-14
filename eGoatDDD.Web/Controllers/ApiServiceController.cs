using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Application.Breeds.Queries;
using Newtonsoft.Json;
using eGoatDDD.Application.GoatBreeds.Models;
using System.Collections.Generic;
using eGoatDDD.Application.Services.Models;
using eGoatDDD.Application.Services.Queries;
using eGoatDDD.Application.Services.Commands;
using System;

namespace eGoatDDD.Web.Controllers
{
    public class ApiServiceController : BaseController
    {
        // Use JWT
        // [Authorize(Policy = "CanEdits")]

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="GoatId"></param>
        /// <param name="Type"></param>
        /// <param name="Category"></param>
        /// <param name="Description"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        [Route("api/service/put")]
        [HttpPost]
        public async Task<JsonResult> Put(long GoatId, string Type, string Category, string Description, DateTime Start, DateTime End)
        {
            CreateServiceCommand createServiceCommand = new CreateServiceCommand
            {
                GoatId = GoatId,
                Category = Category,
                Description = Description,
                Start = Start,
                End = End,
                Type = Type
            };

            ServiceViewModel service = await _mediator.Send(createServiceCommand);

            if (service != null)
            {
                if (service.Service != null)
                {
                    return Json(new { error = 0, service = service.Service });
                }
            }

            return Json(new { error = 1, response = "No service." });
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="GoatId"></param>
        /// <param name="Type"></param>
        /// <param name="Category"></param>
        /// <param name="Description"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        [Route("api/service")]
        [HttpPost]
        public async Task<JsonResult> Get(long GoatId)
        {
            ServicesListViewModel service = await _mediator.Send(new GetServicesQuery(GoatId));

            if (service != null)
            {
                if (service.Services != null)
                {
                    return Json(new { error = 0, service = service.Services.OrderByDescending(s => s.Start).First() });
                }
            }

            return Json(new { error = 1, response = "No service." });
        }

        [Route("api/goat/services")]
        [HttpPost]
        public async Task<JsonResult> GetServices(long GoatId)
        {
            ServicesListViewModel service = await _mediator.Send(new GetServicesQuery(GoatId));

            if (service != null)
            {
                if (service.Services != null)
                {
                    return Json(new { error = 0, services = service.Services.OrderByDescending(s => s.Start) });
                }
            }

            return Json(new { error = 1, response = "No service." });
        }

    }

}
