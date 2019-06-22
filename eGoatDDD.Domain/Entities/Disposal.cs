﻿using System;
using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public enum DisposeType
    {
        Slaughter = 1,
        SoldWholeLocal = 2,
        SoldWholeOnline = 3,
        Death = 4,
        Other = 99
    }

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
