using eGoatDDD.Application.Loans.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.Loans.Commands
{
    public class UpdateLoanCommand : IRequest<LoanViewModel>
    {
        public long LoanId { get; set; }

        public string LesseeId { get; set; }
       
    }
}