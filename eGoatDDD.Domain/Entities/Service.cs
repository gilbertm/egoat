using System;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.Entities
{
    public class Service
    {

        public long ServiceId { get; set; }

        public long GoatId { get; set; }
        
        // medical
        // recreational
        public string Type { get; set; }

        // medical - vaccine
        // medical - deworming
        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }


        public virtual Goat Goat { get; set; }

    }
}
