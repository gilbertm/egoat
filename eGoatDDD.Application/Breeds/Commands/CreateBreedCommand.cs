using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using System;

namespace eGoatDDD.Application.Breeds.Commands
{
    public class CreateBreedCommand : IRequest<BreedViewModel>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }
    }
}