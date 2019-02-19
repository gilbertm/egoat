using MediatR;
using eGoatDDD.Application.Products.Models;

namespace eGoatDDD.Application.Products.Queries
{
    public class GetProductQuery : IRequest<ProductViewModel>
    {
        public GetProductQuery()
        {
        }

        public GetProductQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
