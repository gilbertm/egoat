using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class GoatBreed
    {
        public long GoatId { get; set; }

        public int BreedId { get; set; }

        public float Percentage { get; set; }

        public virtual Goat Goat { get; set; }

        public virtual Breed Breed { get; set; }

    }
}
