﻿using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Goat : BaseEntity
    {
        public Goat()
        {
            Parents = new HashSet<Parent>();
            Births = new HashSet<Birth>();
            Services = new HashSet<GoatService>();

            GoatBreeds = new HashSet<GoatBreed>();
        }
        
        public int ColorId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }

        public string Picture { get; set; }

        public char Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        // one-to-one
        public virtual Color Color { get; set; }

        // zero-to-one
        public virtual Disposal Disposal { get; set; }

        // one-to-many
        public virtual ICollection<Parent> Parents { get; set; }

        // one-to-many
        public virtual ICollection<Birth> Births { get; private set; }

        public virtual ICollection<GoatService> Services { get; private set; }

        // many-to-many
        public virtual ICollection<GoatBreed> GoatBreeds { get; private set; }

    }
}
