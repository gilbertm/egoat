using eGoatDDD.Application.Applicants.Models;
using eGoatDDD.Application.Applicants.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Applicants.Commands
{
    public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, ApplicantViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateApplicantCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ApplicantViewModel> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
        {
            var entity = new Applicant
            {
                LoanId = request.LoanId,
                ApplicantLesseeId = request.ApplicantLesseeId,
                Flag = request.Flag,
                Reason = request.Reason
            };

            _context.Applicants.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetApplicantQuery(entity.LoanId, entity.ApplicantLesseeId), cancellationToken);
        }
    }
}