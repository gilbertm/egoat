using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace eGoatDDD.Web.Controllers
{
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
                    return Json(new { error = 0, response = goat.Goat });
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
    }

}
