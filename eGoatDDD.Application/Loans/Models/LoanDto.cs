using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Loans.Models
{
    public class LoanDto
    {
        public long LoanId { get; set; }

        public string LesseeId { get; set; }

        public DateTime Created { get; set; }

        public LoanDetail LoanDetail { get; set; }

        public ICollection<Applicant> Applicants { get; set; }
        
        public static Expression<Func<Loan, LoanDto>> Projection
        {
            get
            {
                return l => new LoanDto
                {
                    LoanId = l.Id,
                    LesseeId = l.LesseeId,
                    Created = l.Created,
                    LoanDetail = l.LoanDetail,
                    Applicants = l.Applicants, 

                };
            }
        }

        public static LoanDto Create(Loan loan)
        {
            return Projection.Compile().Invoke(loan);
        }
    }
}