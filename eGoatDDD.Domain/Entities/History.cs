using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Domain.Entities
{
    public class History : BaseEntity 
    {
        public History()
        {
            HistoryResources = new HashSet<HistoryResource>();
        }

        public long GoatId { get; set; }
    
        public string Note { get; set; }

        public virtual Goat Goat { get; set; }

        public virtual IEnumerable<HistoryResource> HistoryResources { get; set; }
    }
}
