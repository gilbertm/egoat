using MediatR;
using eGoatDDD.Application.Products.Models;

namespace eGoatDDD.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<ProductsListViewModel>
    {
        public GetAllProductsQuery()
        {

        }

        public GetAllProductsQuery(string lenderId)
        {
            LenderId = lenderId;
        }

        public string LenderId;
    }
}
