using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eGoatDDD.Persistence;
using eGoatDDD.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eGoatDDD.Application.GoatResources.Queries
{
    public class GetGoatResourceQueryHandler : IRequestHandler<GetGoatResourceQuery, bool>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetGoatResourceQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(GetGoatResourceQuery request, CancellationToken cancellationToken)
        {
            if (request.FileName != null)
            {
                Resource resource = await _context.Resources
                    .Where(r => r.Filename.ToLower().Contains(request.FileName))
                    .SingleOrDefaultAsync();

                if (resource != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
