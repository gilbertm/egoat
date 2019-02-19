using MediatR;
using eGoatDDD.Application.LoanDetails.Models;

namespace eGoatDDD.Application.LoanDetails.Queries
{
    public class GetLoanDetailQuery : IRequest<LoanDetailViewModel>
    {
        public GetLoanDetailQuery(long loanId, long productId, string lenderId)
        {
            LoanId = loanId;
            ProductId = productId;
            LenderId = lenderId;
        }

        public long LoanId { get; set; }
        public long ProductId { get; set; }
        public string LenderId { get; set; }
    }
}
