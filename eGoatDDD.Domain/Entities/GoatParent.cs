using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class GoatParent
    {

        public int Id { get; set; }

        public long GoatId { get; set; }

        public bool isMom { get; set; }

        public virtual Goat Goat { get; set; }

    }
}
