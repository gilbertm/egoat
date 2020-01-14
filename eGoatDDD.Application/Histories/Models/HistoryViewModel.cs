using System;

namespace eGoatDDD.Application.Hestories.Models
{
    public class HistoryViewModel
    {
        public ServiceDto Service { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
