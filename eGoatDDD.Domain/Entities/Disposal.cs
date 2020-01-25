using eGoatDDD.Domain.Constants;
using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    
    public class Disposal
    {
        public long Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; }
        public DateTime DisposedOn { get; set; }

        public DisposeType Type { get; set; }

        public string Reason { get; set; }

    }
}
