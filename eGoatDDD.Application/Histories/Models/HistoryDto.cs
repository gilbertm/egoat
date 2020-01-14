using eGoatDDD.Domain.Constants;
using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Histories.Models
{
    public class HistoryDto
    {
        public long Id { get; set; }
        public Guid UniqeId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public long GoatId { get; set; }

        public HistoryType Type { get; set; }
        public string Note { get; set; }

        public virtual Goat Goat { get; set; }

        public static Expression<Func<History, HistoryDto>> Projection
        {
            get
            {

                return h => new HistoryDto
                {
                    Id = h.Id,
                    GoatId = h.GoatId,
                    Type = h.Type,
                    Created = h.Created,
                    Modified = h.Modified,
                    UniqeId = h.UniqeId,
                    Note = h.Note
                };
            }
        }

        public static HistoryDto Create(History history)
        {
            return Projection.Compile().Invoke(history);
        }
    }
}