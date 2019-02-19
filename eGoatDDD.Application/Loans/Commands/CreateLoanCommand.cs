using eGoatDDD.Application.Loans.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.Loans.Commands
{
    public class CreateLoanCommand : IRequest<LoanViewModel>
    {
        public long ProductId { get; set; }

        public string LesseeId { get; set; }

        public DateTime Created { get; set; }

    }
}