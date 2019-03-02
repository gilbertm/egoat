using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Color
    {
        public Color()
        {
            Goats = new HashSet<Goat>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Goat> Goats { get; set;  }
    }
}
