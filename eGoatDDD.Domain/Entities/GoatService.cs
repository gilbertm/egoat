using System;

namespace eGoatDDD.Domain.Entities
{

    public class GoatService
    {

        public long Id { get; set; }
        
        // medical
        // recreational
        public string Service { get; set; }

        // medical: deworming, vaccine
        // recreatinal: spa
        public string Category { get; set; }
        
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual Goat Goat { get; set; }

    }
}
