using eGoatDDD.Application.Applicants.Models;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Applicants.Queries
{
    public class GetAllApplicantsQueryHandler : IRequestHandler<GetAllApplicantsQuery, ApplicantListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAllApplicantsQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicantListViewModel> Handle(GetAllApplicantsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Set view model state based on user permissions.
            var model = new ApplicantListViewModel
            {
                Applicants = await _context.Applicants
                    .Select(ApplicantDto.Projection)
                    .OrderBy(l => l.LoanId)
                    .ThenBy(u => u.ApplicantLesseeId)
                    .ToListAsync(cancellationToken),
                CreateEnabled = true
            };

            return model;
        }
    }
}
