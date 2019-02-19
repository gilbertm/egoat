using eGoatDDD.Application.Applicants.Models;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Applicants.Queries
{
    public class GetApplicantQueryHandler : IRequestHandler<GetApplicantQuery, ApplicantViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetApplicantQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicantViewModel> Handle(GetApplicantQuery request, CancellationToken cancellationToken)
        {
            // TODO: Set view model state based on user permissions.
            var model = new ApplicantViewModel
            {
                Applicant = await _context.Applicants
                    .Select(ApplicantDto.Projection)
                    .Where(a => a.LoanId == request.LoanId && a.ApplicantLesseeId == request.ApplicantLesseeId)
                    .SingleOrDefaultAsync(cancellationToken),

                EditEnabled = true,
                DeleteEnabled = false
            };

            return model;
        }
    }
}
