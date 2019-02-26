using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eGoatDDD.Web.Controllers
{
    [Route("api/goat")]
    public class ApiGoatController : BaseController
    {
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
    }
}
