using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.WebMVC.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eGoatDDD.WebMVC.Controllers
{
    public class ProductController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            GoatsListViewModel goatsListViewModel = await _mediator.Send(new GetAllGoatsQuery());

            return CreatedAtAction("GetProducts", new { }, goatsListViewModel);
        }

    }
}
