using eGoatDDD.Application.Loans.Models;
using eGoatDDD.Application.Loans.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Loans.Commands
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateLoanCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LoanViewModel> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var entity = new Loan
            {
                Id = 0,
                LesseeId = request.LesseeId,
                Created = DateTime.Now
            };

            _context.Loans.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetLoanQuery(entity.Id), cancellationToken);
        }
    }
}