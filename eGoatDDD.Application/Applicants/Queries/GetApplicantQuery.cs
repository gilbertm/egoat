using eGoatDDD.Application.Applicants.Models;
using MediatR;

namespace eGoatDDD.Application.Applicants.Queries
{
    public class GetApplicantQuery : IRequest<ApplicantViewModel>
    {
        public GetApplicantQuery(long loanId, string applicantLesseeId)
        {
            LoanId = loanId;
            ApplicantLesseeId = applicantLesseeId;
        }

        public long LoanId { get; set; }

        public string ApplicantLesseeId { get; set; }
    }
}
