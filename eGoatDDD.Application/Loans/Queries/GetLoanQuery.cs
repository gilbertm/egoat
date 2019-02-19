using MediatR;
using eGoatDDD.Application.Loans.Models;

namespace eGoatDDD.Application.Loans.Queries
{
    public class GetLoanQuery : IRequest<LoanViewModel>
    {

        public GetLoanQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
