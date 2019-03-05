using eGoatDDD.Application.GoatBreeds.Models;
using eGoatDDD.Application.Parents.Models;
using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatNonDtoViewModel
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }

        public string Picture { get; set; }

        public char Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        // one-to-one
        public Color Color { get; set; }

        // zero-to-one
        public Disposal Disposal { get; set; }

        // one-to-many
        public ParentsListViewModel Parents { get; set; }

        // one-to-many
        public GoatsListViewModel Siblings { get; set; }

        // one-to-many
        public ICollection<Birth> Births { get; set; }

        public ICollection<Service> Services { get; set; }

        // many-to-many
        public IList<GoatBreedViewModel> Breeds { get; set; }
        
        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
