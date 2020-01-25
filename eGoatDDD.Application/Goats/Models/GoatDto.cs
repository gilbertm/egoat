﻿using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using eGoatDDD.Application.Parents.Models;
using eGoatDDD.Application.GoatBreeds.Models;
using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Application.Disposals.Models;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatDto
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }

        public char Gender { get; set; }

        public ICollection<GoatResourceDto> GoatResources { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        public ICollection<ParentDto> Parents { get; set; }

        public ColorDto Color { get; private set; }

        public DisposalDto Disposal { get; private set; }

        public ICollection<GoatBreedDto> GoatBreeds { get; set; }

        public GoatsListViewModel Siblings { get; set; }

        public static Expression<Func<Goat, GoatDto>> Projection
        {
            get
            {

                return p => new GoatDto
                {
                    Id = p.Id,
                    DisposalId = p.DisposalId,
                    ColorId = p.ColorId,
                    Gender = p.Gender,
                    Code = p.Code,
                    BirthDate = p.BirthDate,
                    Description = p.Description,
                    Parents = p.Parents.Count() > 0 ? p.Parents.Select(parent => ParentDto.Create(parent)).ToList() : null,
                    GoatResources = p.GoatResources.Count() > 0 ? p.GoatResources.Select(resource => GoatResourceDto.Create(resource)).ToList() : null,
                    Color = ColorDto.Create(p.Color),
                    Disposal = p.DisposalId.HasValue == true ? (p.DisposalId.Value > 0 ? DisposalDto.Create(p.Disposal) : null) : null,
                    GoatBreeds = p.GoatBreeds.Select(breed => GoatBreedDto.Create(breed)).ToList(),
                };
            }
        }

        public static GoatDto Create(Goat goat)
        {
            return Projection.Compile().Invoke(goat);
        }
    }
}