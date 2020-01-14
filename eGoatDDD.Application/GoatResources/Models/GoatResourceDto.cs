using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Colors.Models
{
    public class GoatResourceDto
    {
        public long GoatId { get; set; }

        public Guid ResourceId { get; set; }

        public string Filename { get; set; }

        public string Location { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Modified { get; set; }

        public static Expression<Func<GoatResource, GoatResourceDto>> Projection
        {
            get
            {
               
                return gr => new GoatResourceDto
                {
                    GoatId = gr.GoatId,
                    ResourceId = gr.ResourceId,
                    Filename = gr.Resource.Filename,
                    Location = gr.Resource.Location,
                    Created = gr.Created,
                    Modified = gr.Modified
                };
            }
        }

        public static GoatResourceDto Create(GoatResource GoatResource)
        {
            return Projection.Compile().Invoke(GoatResource);
        }
    }
}