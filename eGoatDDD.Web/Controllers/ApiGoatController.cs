using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Application.GoatBreeds.Models;
using System.Collections.Generic;

namespace eGoatDDD.Web.Controllers
{
    // Use JWT
    // [Authorize(Policy = "CanEdits")]
    public class ApiGoatController : BaseController
    {
        [Route("api/goat")]
        [HttpPost]
        public async Task<JsonResult> Goat(long Id)
        {
            GoatViewModel goat = await _mediator.Send(new GetGoatQuery(Id));

            if (goat != null)
            {
                if (goat.Goat.Id > 0)
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
        public async Task<JsonResult> Siblings(long maternalId, long sireId, long goatId)
        {
            GoatsListViewModel response = await _mediator.Send(new GetGoatSiblingsQuery(maternalId, sireId, goatId));

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
            GoatViewModel goatMaternal = await _mediator.Send(new GetGoatQuery(maternalId));

            GoatViewModel goatSire = await _mediator.Send(new GetGoatQuery(sireId));

            IList<GoatBreedViewModel> unionBloodBreeds = null;

            if (goatMaternal != null && goatSire != null)
            {
                if (goatMaternal.Goat.GoatBreeds != null && goatSire.Goat.GoatBreeds != null)
                {
                    var union = goatMaternal.Goat.GoatBreeds.Union(goatSire.Goat.GoatBreeds).GroupBy(b => new { b.Breed.Id, b.Breed.Name }).ToList();

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
                if (goatMaternal.Goat.GoatBreeds != null)
                {
                    return Json(new { error = 0, breeds = goatMaternal.Goat.GoatBreeds.ToList() });
                }
            }

            if (goatSire != null)
            {
                if (goatSire.Goat.GoatBreeds != null)
                {
                    return Json(new { error = 0, breeds = goatSire.Goat.GoatBreeds.ToList() });
                }
            }


            return Json(new { error = 1, response = "No siblings." });

        }

    }

}
