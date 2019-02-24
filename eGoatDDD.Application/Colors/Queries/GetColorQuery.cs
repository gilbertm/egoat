using MediatR;
using eGoatDDD.Application.Colors.Models;

namespace eGoatDDD.Application.Colors.Queries
{
    public class GetColorQuery : IRequest<ColorViewModel>
    {
        public GetColorQuery()
        {
        }

        public GetColorQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
