using eGoatDDD.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Domain.Entities
{
    public class History : BaseEntity 
    {
        public long GoatId { get; set; }
    
        public string Note { get; set; }

        public HistoryType Type { get; set; }

        public virtual Goat Goat { get; set; }

    }
}
