using MediatR;
using eGoatDDD.Application.Services.Models;

namespace eGoatDDD.Application.Services.Queries
{
    public class GetServiceByServiceQuery : IRequest<ServiceViewModel>
    {
        public GetServiceByServiceQuery()
        {

        }

        public GetServiceByServiceQuery(long serviceId)
        {
            ServiceId = serviceId;
        }

        public long ServiceId { get; set; }
    }
}
