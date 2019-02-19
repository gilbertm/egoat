using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Goat
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string ColorId { get; set; }

        public string Breed { get; set; }

        public string Picture { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime SlaughterDate { get; set; }

        public virtual Color Color { get; set; }

    }
}
