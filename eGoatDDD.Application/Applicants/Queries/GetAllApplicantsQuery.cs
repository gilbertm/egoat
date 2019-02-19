using eGoatDDD.Application.Applicants.Models;
using MediatR;

namespace eGoatDDD.Application.Applicants.Queries
{
    public class GetAllApplicantsQuery : IRequest<ApplicantListViewModel>
    {  

    }
}