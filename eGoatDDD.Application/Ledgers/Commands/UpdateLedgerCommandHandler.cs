using eGoatDDD.Application.Ledgers.Models;
using eGoatDDD.Application.Ledgers.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Ledgers.Commands
{
    public class UpdateLdgerCommandHandler : IRequestHandler<UpdateLedgerCommand, LedgerViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public UpdateLdgerCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LedgerViewModel> Handle(UpdateLedgerCommand request, CancellationToken cancellationToken)
        {
            var ledger = await _context.Ledgers
                 .Where(l => (l.Id == request.LedgerId) && (l.LoanId == request.LoanId) && (l.Position == request.Position))
                 .SingleOrDefaultAsync(cancellationToken);

            if (ledger != null)
            {
                ledger.Amount = request.Amount;
                ledger.Created = DateTime.Now;
                ledger.Remark = request.Remark;

                _context.Ledgers.Update(ledger);

                await _context.SaveChangesAsync(cancellationToken);

                return await _mediator.Send(new GetLedgerQuery(ledger.Id), cancellationToken);

            }

            return null;
        }
    }
}