using System;

namespace eGoatDDD.Application.Services.Models
{
    public class ServiceViewModel
    {
        public ServiceDto Service { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
