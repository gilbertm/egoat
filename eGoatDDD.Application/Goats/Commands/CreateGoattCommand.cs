using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using System;

namespace eGoatDDD.Application.Goats.Commands
{
    public class CreateGoatCommand : IRequest<GoatViewModel>
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public int BreedId { get; set; }

        public string Code { get; set; }

        public string Picture { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime SlaughterDate { get; set; }

        public virtual Color Color { get; set; }

        public virtual Breed Breed { get; set; }
    }
}