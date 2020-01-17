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
        public long ParentId { get; set; }
        public virtual GoatDto Goat { get; set; }
        public virtual GoatDto Parent { get; set; }

        public static Expression<Func<GoatParent, ParentDto>> Projection
        {
            get
            {

                return p => new ParentDto
                {
                    GoatId = p.GoatId,
                    ParentId = p.ParentId,
                    Parent = null,
                    Goat = null
                };
            }
        }

        public static ParentDto Create(GoatParent parent)
        {
            return Projection.Compile().Invoke(parent);
        }
    }
}