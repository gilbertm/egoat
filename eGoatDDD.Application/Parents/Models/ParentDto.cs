using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Parents.Models
{
    public class ParentDto
    {
        public long GoatId { get; set; }

        public string ColorName { get; set; }

        public string Code { get; set; }

        public string Picture { get; set; }

        public char Gender { get; set; }

        public GoatDto Goat { get; set; }

        public ICollection<GoatResourceDto> GoatResources { get; set; }

        public static Expression<Func<Parent, ParentDto>> Projection
        {
            get
            {

                return p => new ParentDto
                {
                    GoatId = p.ParentId,
                    Code = p.Goat.Code,
                    ColorName = p.Goat.Color.Name,
                    Gender = p.Goat.Gender,
                    // GoatResources = p.Goat.GoatResources.Select(resource => GoatResourceDto.Create(resource)).ToList(),
                    Goat = new GoatDto
                    { 
                         GoatResources = p.Goat.GoatResources.Select(resource => GoatResourceDto.Create(resource)).ToList(),
                    }
                };
            }
        }

        public static ParentDto Create(Parent parent)
        {
            return Projection.Compile().Invoke(parent);
        }
    }
}