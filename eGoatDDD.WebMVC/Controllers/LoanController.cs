using eGoatDDD.Application.Applicants.Commands;
using eGoatDDD.Application.Applicants.Models;
using eGoatDDD.Application.LoanDetails.Commands;
using eGoatDDD.Application.LoanDetails.Models;
using eGoatDDD.Application.Loans.Commands;
using eGoatDDD.Application.Loans.Models;
using eGoatDDD.Application.Loans.Queries;
using eGoatDDD.WebMVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Controllers
{
    public class LoanController : BaseController
    {
        private string UserId { get; set; }
        private string Role { get; set; }
        
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                UserId = user.Id.ToString();
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;

            if (claimsIdentity.IsAuthenticated)
            {
                Role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            }

            LoansListViewModel loans = await _mediator.Send(new GetLoansQuery(UserId, Role));
           
            ViewData["Role"] = Role;
            ViewData["UserId"] = UserId;

            return View(loans);
        }

        [Authorize(Policy = "Lenders")]
        [HttpPost]
        public async Task<IActionResult> Broadcast([FromForm] UpdateLoanDetailCommand command)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);

                    UpdateLoanDetailCommand updateLoanDetailCommand = new UpdateLoanDetailCommand
                    {
                        LenderId = user.Id,
                        LoanId = command.LoanId,
                        ProductId = command.ProductId,
                        Status = 1,
                    };

                    LoanDetailViewModel loanDetailViewModel = await _mediator.Send(updateLoanDetailCommand);

                }
            }

            return RedirectToAction("Index", "Lender");
        }

        [Authorize(Policy = "Lessees")]
        [HttpPost]
        public async Task<IActionResult> Apply([FromForm] CreateApplicantCommand command)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);

                    CreateApplicantCommand createApplicantCommand = new CreateApplicantCommand
                    {
                        LoanId = command.LoanId,
                        ApplicantLesseeId = user.Id,
                        Flag = 0,
                        Reason = null
                    };

                    ApplicantViewModel loans = await _mediator.Send(createApplicantCommand);

                }
            }

            return RedirectToAction("Index", "Lessee");
        }

        [Authorize(Policy = "Lessees")]
        [HttpPost]
        public async Task<IActionResult> ApplyCommit([FromForm] UpdateLoanDetailCommand command)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);

                    UpdateLoanDetailCommand updateLoanDetailCommand = new UpdateLoanDetailCommand
                    {
                        LenderId = command.LenderId,
                        LoanId = command.LoanId,
                        ProductId = command.ProductId,
                        Status = command.Status,
                    };

                    LoanDetailViewModel loanDetailViewModel = await _mediator.Send(updateLoanDetailCommand);

                }
            }

            return RedirectToAction("Index", "Lessee");
        }

        [Authorize(Policy = "Lenders")]
        [HttpPost]
        public async Task<IActionResult> Accept([FromForm] UpdateLoanDetailCommand commandLoanDetail, [FromForm] UpdateLoanCommand commandLoan, [FromForm] string submit)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);


                    UpdateLoanDetailCommand updateLoanDetailCommand = new UpdateLoanDetailCommand
                    {
                        LenderId = commandLoanDetail.LenderId == user.Id ? user.Id : null,
                        LoanId = commandLoanDetail.LoanId,
                        ProductId = commandLoanDetail.ProductId,
                        Status = commandLoanDetail.Status,
                    };

                    await _mediator.Send(updateLoanDetailCommand);

                    switch (submit)
                    {
                        case "Accept":
                            {
                                UpdateLoanCommand updateLoanCommand = new UpdateLoanCommand
                                {
                                    LesseeId = commandLoan.LesseeId,
                                    LoanId = commandLoan.LoanId
                                };

                                await _mediator.Send(updateLoanCommand);

                            }
                            break;

                        case "Cancel":

                            {
                                UpdateLoanCommand updateLoanCommand = new UpdateLoanCommand
                                {
                                    LesseeId = null,
                                    LoanId = commandLoan.LoanId
                                };

                                await _mediator.Send(updateLoanCommand);
                            }

                            break;

                        case "Final and Lock":
                            {
                                // Prepare ledger
                            }

                            break;
                    }

                   

                    

                }
            }

            return RedirectToAction("Index", "Loan");
        }
    }
}