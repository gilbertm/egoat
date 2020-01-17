using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Goat : BaseEntity
    {
        public Goat()
        {
            Parents = new HashSet<GoatParent>();
            Births = new HashSet<Birth>();
            Services = new HashSet<GoatService>();

            GoatBreeds = new HashSet<GoatBreed>();
            GoatResources = new HashSet<GoatResource>();
        }
        
        public int ColorId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }
        
        public char Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        // one-to-one
        public virtual Color Color { get; set; }

        // one-to-many
        public virtual ICollection<GoatParent> Parents { get; set; }

        // one-to-many
        public virtual ICollection<Birth> Births { get; set; }

        public virtual ICollection<GoatService> Services { get; set; }

        // many-to-many
        public virtual ICollection<GoatBreed> GoatBreeds { get; set; }

        public virtual ICollection<GoatResource> GoatResources { get; set; }

        public virtual Disposal Disposal { get; set; }

    }
}
