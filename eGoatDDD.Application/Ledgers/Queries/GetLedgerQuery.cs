using MediatR;
using eGoatDDD.Application.Ledgers.Models;

namespace eGoatDDD.Application.Ledgers.Queries
{
    public class GetLedgerQuery : IRequest<LedgerViewModel>
    {

        public GetLedgerQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
