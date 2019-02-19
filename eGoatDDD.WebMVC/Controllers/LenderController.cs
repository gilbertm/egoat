using eGoatDDD.Domain.Entities;
using eGoatDDD.WebMVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Controllers
{
    [Authorize(Policy = "Lenders")]
    public class LenderController : BaseController
    {

        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);

            var loanDetails = _context.LoanDetails.Where(ld => ld.LenderId == user.Id)
                .Include(p => p.Product)
                .Include(l => l.Loan)
                .Include(l => l.Lender);


            // var loanApplicants = _context.Applicants.Where();


            ViewData["loanDetailsNew"] = loanDetails.Where(ld => ld.Status == 0);
            ViewData["loanDetailsBroadcast"] = loanDetails.Where(ld => ld.Status == 1);

            return View();
        }

    }
}