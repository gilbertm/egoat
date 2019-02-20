using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Goat
    {
        public Goat()
        {
            GoatParents = new HashSet<GoatParent>();
            GoatBirths = new HashSet<GoatBirth>();
            GoatServices = new HashSet<GoatService>();
        }

        public long Id { get; set; }

        public int ColorId { get; set; }

        public int BreedId { get; set; }

        public string Code { get; set; }

        public string Picture { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime SlaughterDate { get; set; }

        public virtual Color Color { get; set; }

        public virtual Breed Breed { get; set; }

        public virtual ICollection<GoatParent> GoatParents { get; private set; }

        public virtual ICollection<GoatBirth> GoatBirths { get; private set; }

        public virtual ICollection<GoatService> GoatServices { get; private set; }

    }
}
