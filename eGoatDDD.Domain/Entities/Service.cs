using System;

namespace eGoatDDD.Domain.Entities
{

    public class Service
    {

        public long Id { get; set; }
        
        // medical
        // recreational
        public string Type { get; set; }

        // medical: deworming, vaccine
        // recreatinal: spa
        public string Category { get; set; }
        
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

    }
}
