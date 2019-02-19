using eGoatDDD.Application.Categories.Models;
using eGoatDDD.Application.Categories.Queries;
using eGoatDDD.Application.LoanDetails.Commands;
using eGoatDDD.Application.LoanDetails.Models;
using eGoatDDD.Application.Loans.Commands;
using eGoatDDD.Application.Loans.Models;
using eGoatDDD.Application.Products.Commands;
using eGoatDDD.Application.Products.Models;
using eGoatDDD.Application.Products.Queries;
using eGoatDDD.WebMVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Controllers
{
    public class ProductController : BaseController
    {
        private CategoriesListViewModel categoriesListView = null;

        public async Task<IActionResult> Index()
        {
            ProductsListViewModel productListViewModel = await _mediator.Send(new GetAllProductsQuery());

            return CreatedAtAction("GetProducts", new { }, productListViewModel);

            // TODO://
            // this should return a partial view
            // SignalR should be used for
            // non-reloading technique
            // return View();
        }

        [Authorize(Policy = "Lenders")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categoriesListView = await _mediator.Send(new GetAllCategoriesQuery());

            ViewData["categories"] = categoriesListView.Categories;

            return View();
        }

        [Authorize(Policy = "Lenders")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
        {
            if (command.CategoryId == 0)
            {
                ModelState.AddModelError("Id", "Category is required");
            }

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);

                    command.ProductId = 0;
                    command.Created = DateTime.Now;
                    command.Updated = DateTime.Now;

                    ProductViewModel productViewModel = await _mediator.Send(command);

                    CreateLoanCommand createLoanCommand = new CreateLoanCommand
                    {
                        ProductId = productViewModel.Product.ProductId,
                        Created = DateTime.Now,
                        LesseeId = null
                    };

                    LoanViewModel loanViewModel = await _mediator.Send(createLoanCommand);

                    CreateLoanDetailCommand createLoanDetailCommand = new CreateLoanDetailCommand
                    {
                        ProductId = productViewModel.Product.ProductId,
                        LoanId = loanViewModel.Loan.LoanId,
                        LenderId = user.Id,
                        Status = command.Save.Equals("Save and Broadcast") ? 1 : 0,
                        IsDiminishing = true,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    LoanDetailViewModel loanDetailViewModel = await _mediator.Send(createLoanDetailCommand);

                }

                return RedirectToAction("Index", "Lender");
            }

            ViewData["categories"] = categoriesListView.Categories;

            return View();
        }

    }
}
