using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGoatDDD.Domain.Entities
{
    public class GoatParent
    {
        public long GoatId { get; set; }

        public long ParentId { get; set; }

        public virtual Goat Goat { get; set; }

        public virtual Goat Parent { get; set; }

    }
}
