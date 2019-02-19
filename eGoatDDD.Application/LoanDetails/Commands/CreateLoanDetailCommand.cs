using eGoatDDD.Application.LoanDetails.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.LoanDetails.Commands
{
    public class CreateLoanDetailCommand : IRequest<LoanDetailViewModel>
    {
        public long LoanId { get; set; }

        public long ProductId { get; set; }

        public string LenderId { get; set; }

        public int Status { get; set; }

        public int Month { get; set; }

        public float Interest { get; set; }

        public bool IsDiminishing { get; set; }

        public DateTime StartOfPayment { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

    }
}