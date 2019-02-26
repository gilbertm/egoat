using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Breed
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public virtual ICollection<GoatBreed> GoatBreeds { get; private set; }
    }
}
