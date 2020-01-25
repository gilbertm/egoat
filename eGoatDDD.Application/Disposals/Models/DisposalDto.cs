using eGoatDDD.Domain.Constants;
using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Disposals.Models
{
    public class DisposalDto
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime DisposedOn { get; set; }

        public DisposeType Type { get; set; }

        public string Reason { get; set; }

        public static Expression<Func<Disposal, DisposalDto>> Projection
        {
            get
            {

                return d => new DisposalDto
                {
                    Id = d.Id,
                    Created = d.Created,
                    Modified = d.Modified,
                    DisposedOn = d.DisposedOn,
                    Reason = d.Reason,
                    Type = d.Type
                };
            }
        }

        public static DisposalDto Create(Disposal Disposal)
        {
            return Projection.Compile().Invoke(Disposal);
        }
    }
}