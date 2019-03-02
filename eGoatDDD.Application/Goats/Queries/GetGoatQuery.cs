using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatQuery : IRequest<GoatNonDtoViewModel>
    {
        public GetGoatQuery()
        {
        }

        public GetGoatQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
