using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Domain.Entities
{
    public class HistoryResource : BaseEntity
    {
        public long HistoryId { get; set; }

        // store the URL
        public string ResourceURL { get; set; }

        public virtual History History { get; set; }
    }
}
