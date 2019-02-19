using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.LoanDetails.Models;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.LoanDetails.Queries
{
    public class GetLoanDetailQueryHandler : MediatR.IRequestHandler<GetLoanDetailQuery, LoanDetailViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetLoanDetailQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<LoanDetailViewModel> Handle(GetLoanDetailQuery request, CancellationToken cancellationToken)
        {
            var loanDetail = await _context.LoanDetails
                .Select(LoanDetailDto.Projection)
                .Where(ld => ld.LoanId == request.LoanId && ld.ProductId == request.ProductId && ld.LenderId == request.LenderId)
                .SingleOrDefaultAsync(cancellationToken);

            if (loanDetail == null)
            {
                throw new NotFoundException(nameof(LoanDetail), request.LoanId);
            }

            var model = new LoanDetailViewModel
            {
                LoanDetail = loanDetail
            };

            return model;
        }
    }
}
