using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Resource
    {
        
        public Guid ResourceId { get; set; }

        public string Filename { get; set; }

        public string Location { get; set; }

        public virtual ICollection<GoatResource> GoatResources { get; private set; }

    }
}
