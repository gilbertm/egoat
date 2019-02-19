using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Loans.Models;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.Loans.Queries
{
    public class GetLoanQueryHandler : MediatR.IRequestHandler<GetLoanQuery, LoanViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetLoanQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<LoanViewModel> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans
                .Select(LoanDto.Projection)
                .Where(l => l.LoanId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (loan == null)
            {
                throw new NotFoundException(nameof(Loan), request.Id);
            }

            var model = new LoanViewModel
            {
                Loan = loan
            };

            return model;
        }
    }
}
