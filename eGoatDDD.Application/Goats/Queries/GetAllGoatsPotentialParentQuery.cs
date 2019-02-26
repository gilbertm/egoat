using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetAllGoatsPotentialParentQuery : IRequest<GoatsListViewModel>
    {
        public GetAllGoatsPotentialParentQuery()
        {

        }

        public GetAllGoatsPotentialParentQuery(bool isSire = false)
        {
            IsSire = isSire;
        }

        public bool IsSire { get; set; }
    }
}
