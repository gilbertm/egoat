using eGoatDDD.Application.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace eGoatDDD.WebMVC.ViewComponents
{
    public class LenderGetAllProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ProductsListViewModel productsListViewModel)
        {
            return View(productsListViewModel);
        }
    }
}
