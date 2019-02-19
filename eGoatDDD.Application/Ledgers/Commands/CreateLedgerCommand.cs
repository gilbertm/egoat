using eGoatDDD.Application.Ledgers.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.Ledgers.Commands
{
    public class CreateLedgerCommand : IRequest<LedgerViewModel>
    {
        public long LedgerId { get; set; }

        public long LoanId { get; set; }

        public int Position { get; set; }

        public decimal Amount { get; set; }

        public DateTime Created { get; set; }

        public string Remark { get; set; }

    }
}