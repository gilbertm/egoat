using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Persistence;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Application.Breeds.Models;
using System.Collections.Generic;
using eGoatDDD.Application.GoatBreeds.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatQueryHandler : MediatR.IRequestHandler<GetGoatQuery, GoatNonDtoViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetGoatQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<GoatNonDtoViewModel> Handle(GetGoatQuery request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var goat = await _context.Goats
                    .Where(g => g.Id == request.Id)
                    .Include(c => c.Color)
                    .Include(gb => gb.GoatBreeds)
                        .ThenInclude(b => b.Breed)
                    .SingleOrDefaultAsync(cancellationToken);

                Color color = new Color
                {
                    Id = goat.Color.Id,
                    Name = goat.Color.Name,
                    Description = goat.Color.Description
                };

                List<GoatBreedViewModel> breeds = null;
                if (goat.GoatBreeds.Count() > 0)
                {
                    breeds = new List<GoatBreedViewModel>();

                    foreach (var item in goat.GoatBreeds)
                    {
                        breeds.Add(new GoatBreedViewModel
                        {
                               Id = item.Breed.Id,                               
                               Name = item.Breed.Name,
                               Percentage = item.Percentage
                        });
                    }
                }

                if (goat == null)
                {
                    throw new NotFoundException(nameof(Goat), request.Id);
                }

                // TODO: Set view model state based on user permissions.
                var model = new GoatNonDtoViewModel
                {
                    Id = goat.Id,
                    BirthDate = goat.BirthDate,
                    Code = goat.Code,
                    ColorId = goat.ColorId,
                    Gender = goat.Gender,
                    Picture = goat.Picture,
                    Color = color,
                    Breeds = breeds,
                    EditEnabled = true,
                    DeleteEnabled = false
                };

                return model;
            }

            return null;
        }
    }
}
