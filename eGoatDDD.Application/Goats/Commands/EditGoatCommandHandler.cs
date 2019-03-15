using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace eGoatDDD.Application.Goats.Commands
{
    public class EditGoatCommandHandler : IRequestHandler<EditGoatCommand, bool>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public EditGoatCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EditGoatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Goat goat = _context.Goats.
                    Where(g => g.Id == request.Id).SingleOrDefault();

                if (goat != null)
                {
                    goat.ColorId = request.ColorId;
                    goat.Code = request.Code;
                    goat.Gender = request.Gender;
                    goat.Picture = request.Picture;
                    goat.BirthDate = request.BirthDate;
                    goat.Description = request.Description;
                };

                await _context.SaveChangesAsync(cancellationToken);

                if (request.MaternalId.HasValue)
                    if (request.MaternalId.Value > 0)
                    {
                        Parent parent = _context.Parents.Where(p => (p.GoatId == request.Id) && (p.ParentId == request.MaternalId.Value)).SingleOrDefault();

                        _context.Parents.Remove(parent);

                        _context.Parents.Add(new Parent
                        {
                            ParentId = request.MaternalId.Value,
                            GoatId = goat.Id
                        });
                    }

                if (request.SireId.HasValue)
                    if (request.SireId.Value > 0)
                    {
                        Parent parent = _context.Parents.Where(p => (p.GoatId == request.Id) && (p.ParentId == request.SireId.Value)).SingleOrDefault();

                        _context.Parents.Remove(parent);


                        _context.Parents.Add(new Parent
                        {
                            ParentId = request.SireId.Value,
                            GoatId = goat.Id
                        });
                    }

                List<GoatBreed> goatBreeds = _context.GoatBreeds.Where(breed => (breed.GoatId == request.Id)).ToList();

                _context.GoatBreeds.RemoveRange(goatBreeds);

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