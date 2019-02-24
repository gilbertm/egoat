using MediatR;
using eGoatDDD.Application.Colors.Models;

namespace eGoatDDD.Application.Colors.Queries
{
    public class GetAllColorsQuery : IRequest<ColorsListViewModel>
    {
        public GetAllColorsQuery()
        {

        }
    }
}
