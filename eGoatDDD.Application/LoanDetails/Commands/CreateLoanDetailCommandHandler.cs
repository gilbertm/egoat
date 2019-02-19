using eGoatDDD.Application.LoanDetails.Models;
using eGoatDDD.Application.LoanDetails.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.LoanDetails.Commands
{
    public class CreateLoanDetailCommandHandler : IRequestHandler<CreateLoanDetailCommand, LoanDetailViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateLoanDetailCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LoanDetailViewModel> Handle(CreateLoanDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanDetail
            {
                LenderId = request.LenderId,
                ProductId = request.ProductId,
                LoanId = request.LoanId,
                Created = request.Created,
                Updated = request.Updated
            };

            _context.LoanDetails.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetLoanDetailQuery(entity.LoanId, entity.ProductId, entity.LenderId), cancellationToken);
        }
    }
}