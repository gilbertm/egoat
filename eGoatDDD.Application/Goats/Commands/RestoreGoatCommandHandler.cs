using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using eGoatDDD.Persistence.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace eGoatDDD.Application.Goats.Commands
{
    public class RestoreGoatCommandHandler : IRequestHandler<RestoreGoatCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;


        public RestoreGoatCommandHandler(IUnitOfWork unitOfWork,
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RestoreGoatCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (request.Id > 0)
                    {
                        Goat goat = _context.Goats
                            .Where(g => g.Id == request.Id)
                            .SingleOrDefault();

                        if (goat != null)
                        {
                            var disposalId = goat.DisposalId;

                            goat.DisposalId = null;
                            
                            await _context.SaveChangesAsync(cancellationToken);


                            Disposal disposed = _context.Disposals.Where(d => d.Id == goat.DisposalId).SingleOrDefault();

                            if (disposed is { })
                            {
                                _context.Disposals.Remove(disposed);
                            }

                            
                            await _context.SaveChangesAsync(cancellationToken);

                        }
                    }

                    transaction.Complete();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    _unitOfWork.Rollback();

                    return false;
                }
            }

            return true;
        }
    }
}