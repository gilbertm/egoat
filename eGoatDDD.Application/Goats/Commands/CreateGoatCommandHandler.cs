using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace eGoatDDD.Application.Goats.Commands
{
    public class CreateGoatCommandHandler : IRequestHandler<CreateGoatCommand, bool>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateGoatCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CreateGoatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var goat = new Goat
                {
                    Id = 0,
                    ColorId = request.ColorId,
                    DisposalId = request.DisposalId,
                    Code = request.Code,
                    Gender = request.Gender,
                    Picture = request.Picture,
                    BirthDate = request.BirthDate,
                    Description = request.Description
                };

                _context.Goats.Add(goat);

                await _context.SaveChangesAsync(cancellationToken);

                if (request.MaternalId.HasValue)
                    if (request.MaternalId.Value > 0)
                    {
                        _context.Parents.Add(new Parent
                        {
                            Id = 0,
                            GoatId = goat.Id,
                            ParentId = request.MaternalId.Value
                        });
                    }

                if (request.SireId.HasValue)
                    if (request.SireId.Value > 0)
                    {
                        _context.Parents.Add(new Parent
                        {
                            Id = 0,
                            GoatId = goat.Id,
                            ParentId = request.SireId.Value
                        });
                    }

                for (int i = 0; i < request.BreedId.Count(); i++)
                {
                    _context.GoatBreeds.Add(new GoatBreed
                    {
                        GoatId = goat.Id,
                        BreedId = (int)request.BreedId.ElementAt(i),
                        Percentage = (float)request.BreedPercent.ElementAt(i)
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);

            }
            catch (Exception e)
            {
                return false;
            }


            return true;
        }
    }
}