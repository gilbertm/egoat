using eGoatDDD.Application.Ledgers.Models;
using eGoatDDD.Application.Ledgers.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Ledgers.Commands
{
    public class CreateLedgerCommandHandler : IRequestHandler<CreateLedgerCommand, LedgerViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateLedgerCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LedgerViewModel> Handle(CreateLedgerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Ledger
            {
                Id = 0,
                LoanId = request.LoanId,
                Position = request.Position,
                Amount = request.Amount,
                Remark = request.Remark,
                Created = DateTime.Now
            };

            _context.Ledgers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetLedgerQuery(entity.Id), cancellationToken);
        }
    }
}