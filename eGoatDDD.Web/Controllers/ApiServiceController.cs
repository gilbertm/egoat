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
        /// Update
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="GoatId"></param>
        /// <param name="Type"></param>
        /// <param name="Category"></param>
        /// <param name="Description"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        [Route("api/service/update")]
        [HttpPost]
        public async Task<JsonResult> Update(long ServiceId, long GoatId, string Type, string Category, string Description, DateTime Start, DateTime End)
        {
            UpdateServiceCommand updateServiceCommand = new UpdateServiceCommand
            {
                ServiceId = ServiceId,
                GoatId = GoatId,
                Category = Category,
                Description = Description,
                Start = Start,
                End = End,
                Type = Type
            };

            ServiceViewModel service = await _mediator.Send(updateServiceCommand);

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
        public async Task<JsonResult> Get(long GoatId, int count = 1)
        {
            ServicesListViewModel service = await _mediator.Send(new GetServicesQuery(GoatId));

            if (service != null)
            {
                if (service.Services != null)
                {
                    if (count > 0)
                        return Json(new { error = 0, services = service.Services.OrderByDescending(s => s.Start).Take(count) });
                    else
                        return Json(new { error = 0, services = service.Services.OrderByDescending(s => s.Start) });
                }
            }

            return Json(new { error = 1, response = "No service." });
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ServiceId"></param>
        [Route("api/service/delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(long ServiceId)
        {
            DeleteServiceCommand deleteServiceCommand = new DeleteServiceCommand
            {
                ServiceId = ServiceId
            };

            bool delete = await _mediator.Send(deleteServiceCommand);

            if (delete == true)
            {
                return Json(new { error = 0, service = delete });
            }

            return Json(new { error = 1, response = "Service not found." });
        }

    }

}
