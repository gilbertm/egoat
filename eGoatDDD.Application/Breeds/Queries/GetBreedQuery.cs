using MediatR;
using eGoatDDD.Application.Breeds.Models;

namespace eGoatDDD.Application.Breeds.Queries
{
    public class GetBreedQuery : IRequest<BreedViewModel>
    {
        public GetBreedQuery()
        {
        }

        public GetBreedQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
