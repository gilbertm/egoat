using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Ledgers.Models;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;

namespace eGoatDDD.Application.Ledgers.Queries
{
    public class GetLedgerQueryHandler : IRequestHandler<GetLedgerQuery, LedgerViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetLedgerQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<LedgerViewModel> Handle(GetLedgerQuery request, CancellationToken cancellationToken)
        {
            var ledger = await _context.Ledgers
                .Select(LedgerDto.Projection)
                .Where(l => l.LedgerId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (ledger == null)
            {
                throw new NotFoundException(nameof(Ledger), request.Id);
            }

            var model = new LedgerViewModel
            {
                 Ledger = ledger
            };

            return model;
        }
    }
}
