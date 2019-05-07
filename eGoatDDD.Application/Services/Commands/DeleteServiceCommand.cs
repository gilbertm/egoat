using MediatR;

namespace eGoatDDD.Application.Services.Commands
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public long ServiceId { get; set; }
    }
}