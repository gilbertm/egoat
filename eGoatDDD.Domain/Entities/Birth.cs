using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.Entities
{
    public class Birth
    {
        public long Id { get; set; }

        public long GoatId { get; set; }

        public DateTime Delivery { get; set; }

        public int Alive { get; set; }

        public int Total { get; set; }

        public virtual Goat Goat { get; set; }

    }
}
