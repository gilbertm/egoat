using MediatR;
using eGoatDDD.Application.Breeds.Models;

namespace eGoatDDD.Application.Breeds.Queries
{
    public class GetAllBreedsQuery : IRequest<BreedsListViewModel>
    {
        public GetAllBreedsQuery()
        {

        }
    }
}
