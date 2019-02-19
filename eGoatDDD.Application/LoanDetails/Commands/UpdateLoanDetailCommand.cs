using eGoatDDD.Application.LoanDetails.Models;
using MediatR;

namespace eGoatDDD.Application.LoanDetails.Commands
{
    public class UpdateLoanDetailCommand : IRequest<LoanDetailViewModel>
    {
        public long LoanId { get; set; }

        public long ProductId { get; set; }

        public string LenderId { get; set; }

        public int Status { get; set; }

    }
}