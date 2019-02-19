using eGoatDDD.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Application.Categories.Models
{
    public class CategoriesListViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
