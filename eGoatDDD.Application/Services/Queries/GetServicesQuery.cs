using MediatR;
using eGoatDDD.Application.Services.Models;

namespace eGoatDDD.Application.Services.Queries
{
    public class GetServicesQuery : IRequest<ServicesListViewModel>
    {
        public GetServicesQuery()
        {

        }

        public GetServicesQuery(long goatId)
        {
            GoatId = goatId;
        }

        public long GoatId { get; set; }
    }
}
