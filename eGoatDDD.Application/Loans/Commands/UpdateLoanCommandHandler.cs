using eGoatDDD.Application.Loans.Models;
using eGoatDDD.Application.Loans.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Loans.Commands
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, LoanViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public UpdateLoanCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LoanViewModel> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans
                 .Where(l => l.Id == request.LoanId)
                 .SingleOrDefaultAsync(cancellationToken);

            if (loan != null)
            {
                loan.LesseeId = request.LesseeId;
                _context.Loans.Update(loan);

                await _context.SaveChangesAsync(cancellationToken);

                return await _mediator.Send(new GetLoanQuery(loan.Id), cancellationToken);

            }

            return null;
        }
    }
}