using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Services.Models
{
    public class ServiceDto
    {
        public long ServiceId { get; set; }

        public long GoatId { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public static Expression<Func<GoatService, ServiceDto>> Projection
        {
            get
            {

                return s => new ServiceDto
                {
                    ServiceId = s.ServiceId,
                    GoatId = s.GoatId,
                    Category = s.Category,
                    Description = s.Description,
                    Type = s.Type,
                     Start = s.Start,
                     End = s.End
                };
            }
        }

        public static ServiceDto Create(GoatService service)
        {
            return Projection.Compile().Invoke(service);
        }
    }
}