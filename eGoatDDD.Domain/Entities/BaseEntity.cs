using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public Guid UniqeId { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; }
    }
}
