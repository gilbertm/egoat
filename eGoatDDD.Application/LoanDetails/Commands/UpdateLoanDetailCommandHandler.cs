using eGoatDDD.Application.LoanDetails.Models;
using eGoatDDD.Application.LoanDetails.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.LoanDetails.Commands
{
    public class UpdateLoanDetailCommandHandler : IRequestHandler<UpdateLoanDetailCommand, LoanDetailViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public UpdateLoanDetailCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LoanDetailViewModel> Handle(UpdateLoanDetailCommand request, CancellationToken cancellationToken)
        {
            var loanDetail = await _context.LoanDetails
                .Where(ld => ld.LoanId == request.LoanId && ld.ProductId == request.ProductId && ld.LenderId == request.LenderId)
                .SingleOrDefaultAsync(cancellationToken);

            if (loanDetail != null)
            {
                loanDetail.Status = request.Status;

                _context.LoanDetails.Update(loanDetail);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetLoanDetailQuery(loanDetail.LoanId, loanDetail.ProductId, loanDetail.LenderId), cancellationToken);
        }
    }
}