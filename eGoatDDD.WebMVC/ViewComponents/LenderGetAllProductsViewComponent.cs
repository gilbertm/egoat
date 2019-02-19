using eGoatDDD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eGoatDDD.WebMVC.ViewComponents
{
    public class LenderGetAllLoanDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<LoanDetail> loanDetails)
        {
            return View(loanDetails);
        }
    }
}
