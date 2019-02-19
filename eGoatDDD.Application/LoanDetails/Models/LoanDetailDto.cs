using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.LoanDetails.Models
{
    public class LoanDetailDto
    {
        public long LoanId { get; set; }

        public long ProductId { get; set; }

        public string LenderId { get; set; }

        public int Status { get; set; }

        public DateTime StartOfPayment { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }


        public static Expression<Func<LoanDetail, LoanDetailDto>> Projection
        {
            get
            {

                return ld => new LoanDetailDto
                {
                    LoanId = ld.LoanId,
                    ProductId = ld.ProductId,
                    LenderId = ld.LenderId,
                    StartOfPayment = ld.StartOfPayment,
                    Created = ld.Created,
                    Updated = ld.Updated,
                    Status = ld.Status
                };
            }
        }

        public static LoanDetailDto Create(LoanDetail loanDetail)
        {
            return Projection.Compile().Invoke(loanDetail);
        }
    }
}