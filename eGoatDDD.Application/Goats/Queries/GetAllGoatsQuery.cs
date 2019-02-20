using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetAllGoatsQuery : IRequest<GoatsListViewModel>
    {
        public GetAllGoatsQuery()
        {

        }
    }
}
