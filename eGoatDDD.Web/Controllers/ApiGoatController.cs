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

namespace eGoatDDD.Web.Controllers
{
    public class ApiGoatController : BaseController
    {
        [Route("api/goat")]
        [HttpPost]
        public async Task<JsonResult> Goat(long Id)
        {
            GoatNonDtoViewModel goat = await _mediator.Send(new GetGoatQuery(Id));

            if (goat != null)
            {
                if (goat.Id > 0)
                {
                    return Json(new { error = 0, response = goat });
                }
            }

            return Json(new { error = 1, response = "Goat does not exists." });
        }


        [Route("api/goat/code")]
        [HttpPost]
        public async Task<JsonResult> CodeIsValid(int colorId, string code)
        {
            bool response = false;

            response = await _mediator.Send(new GetGoatCodeQuery(colorId, code));

            if (response)
            {
                return Json(new { error = 0, response = "success" });
            }

            return Json(new { error = 1, response = "CodeId + Code exists." });
        }

        [Route("api/goat/siblings")]
        [HttpPost]
        public async Task<JsonResult> Siblings(long maternalId, long sireId)
        {
            GoatsListViewModel response = await _mediator.Send(new GetGoatSiblingsQuery(maternalId, sireId));

            if (response.Goats.Count() > 0)
            {
                return Json(new { error = 0, response.Goats });
            }

            return Json(new { error = 1, response = "No siblings." });
        }

        [Route("api/goat/parent-breeds")]
        [HttpPost]
        public async Task<JsonResult> ParentBreeds(long maternalId, long sireId)
        {
            GoatNonDtoViewModel goatMaternal = await _mediator.Send(new GetGoatQuery(maternalId));

            GoatNonDtoViewModel goatSire = await _mediator.Send(new GetGoatQuery(sireId));

            IList<GoatBreedViewModel> unionBloodBreeds = null;

            if (goatMaternal != null && goatSire != null)
            {
                if (goatMaternal.Breeds != null && goatSire.Breeds != null)
                {
                    var union = goatMaternal.Breeds.Union(goatSire.Breeds).GroupBy(b => new { b.Id, b.Name }).ToList();

                    unionBloodBreeds = (from g in union
                                        select new GoatBreedViewModel
                                        {
                                            Id = g.Key.Id,
                                            Name = g.Key.Name,
                                            Percentage = g.Sum(i => i.Percentage) / 2
                                        }).ToList();

                    return Json(new { error = 0, breeds = unionBloodBreeds });
                }
            }

            if (goatMaternal != null)
            {
                if (goatMaternal.Breeds != null)
                {
                    return Json(new { error = 0, breeds = goatMaternal.Breeds.ToList() });
                }
            }

            if (goatSire != null)
            {
                if (goatSire.Breeds != null)
                {
                    return Json(new { error = 0, breeds = goatSire.Breeds.ToList() });
                }
            }


            return Json(new { error = 1, response = "No siblings." });

        }

    }

}
