using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Domain.Entities
{
    public class HistoryResource
    {
        public long Id { get; set; }

        public long HistoryId { get; set; }

        // created/updated are crucial dates
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        // store the URL
        public string ResourceURL { get; set; }

        public virtual History History { get; set; }
    }
}
