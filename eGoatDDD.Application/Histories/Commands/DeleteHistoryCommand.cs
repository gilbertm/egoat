using MediatR;

namespace eGoatDDD.Application.Histories.Commands
{
    public class DeleteServiceCommand : IRequest<bool>
    {
        public long HistoryId { get; set; }
    }
}