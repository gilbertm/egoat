using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Ledgers.Models
{
    public class LedgerDto
    {
        public long LedgerId { get; set; }

        public long  LoanId { get; set; }

        public int Position { get; set; }

        public decimal Amount { get; set; }

        public DateTime Created { get; set; }

        public string Remark { get; set; }

        public static Expression<Func<Ledger, LedgerDto>> Projection
        {
            get
            {
                return l => new LedgerDto
                {
                    LedgerId = l.Id,
                    LoanId = l.LoanId,
                    Position = l.Position,
                    Amount = l.Amount,
                    Created = l.Created,
                    Remark = l.Remark 

                };
            }
        }

        public static LedgerDto Create(Ledger ledger)
        {
            return Projection.Compile().Invoke(ledger);
        }
    }
}