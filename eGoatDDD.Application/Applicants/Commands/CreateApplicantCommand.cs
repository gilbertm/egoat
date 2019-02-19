using eGoatDDD.Application.Applicants.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.Applicants.Commands
{
    public class CreateApplicantCommand : IRequest<ApplicantViewModel>
    {
        public long LoanId { get; set; }

        public string ApplicantLesseeId { get; set; }

        public int Flag { get; set; }

        public string Reason { get; set; }
    }
}