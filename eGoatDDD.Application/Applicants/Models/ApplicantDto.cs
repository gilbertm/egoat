using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Applicants.Models
{
    public class ApplicantDto
    {
        public long LoanId { get; set; }

        public string ApplicantLesseeId { get; set; }

        public int Flag { get; set; }

        public string Reason { get; set; }

        public static Expression<Func<Applicant, ApplicantDto>> Projection
        {
            get
            {
                return a => new ApplicantDto
                {
                    LoanId = a.LoanId,
                    ApplicantLesseeId = a.ApplicantLesseeId,
                    Flag = a.Flag,
                    Reason = a.Reason
                };
            }
        }

        public static ApplicantDto Create(Applicant applicant)
        {
            return Projection.Compile().Invoke(applicant);
        }
    }
}