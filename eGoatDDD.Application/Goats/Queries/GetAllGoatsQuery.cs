using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetAllGoatsQuery : IRequest<GoatsListNonDtoViewModel>
    {
        public GetAllGoatsQuery()
        {

        }

        public GetAllGoatsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string Filter { get; set; }
    }
}
