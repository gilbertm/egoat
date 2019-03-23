using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class GoatResource
    {

        public long GoatId { get; set; }

        public Guid ResourceId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Modified { get; set; }

        public virtual Goat Goat { get; set; }

        public virtual Resource Resource { get; set; }

    }
}
