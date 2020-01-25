using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Constants;
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
    public class DeleteGoatCommandHandler : IRequestHandler<DeleteGoatCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;


        public DeleteGoatCommandHandler(IUnitOfWork unitOfWork,
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteGoatCommand request, CancellationToken cancellationToken)
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
                            Disposal disposal = new Disposal
                            {
                                DisposedOn = DateTime.Parse(request.DisposedOn.ToString()),
                                Reason = request.Reason,
                                Type = (DisposeType)Enum.Parse(typeof(DisposeType), request.Type.ToString(), true),
                                Modified = DateTime.Now,
                                Created = DateTime.Now,
                            };

                            _context.Disposals.Add(disposal);

                            await _context.SaveChangesAsync(cancellationToken);

                            goat.DisposalId = disposal.Id;

                            await _context.SaveChangesAsync(cancellationToken);

                        }
                    }

                    await _context.SaveChangesAsync(cancellationToken);

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