using eGoatDDD.Application.Categories.Models;
using MediatR;

namespace eGoatDDD.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<CategoriesListViewModel>
    {  
    }
}