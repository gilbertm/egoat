using MediatR;
using eGoatDDD.Application.Goats.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatCodeQuery : IRequest<bool>
    {
        public GetGoatCodeQuery()
        {
        }

        public GetGoatCodeQuery(int colorId, string code)
        {
            ColorId = colorId;
            Code = code;
        }

        public int ColorId { get; set; }

        public string Code { get; set; }
    }
}
