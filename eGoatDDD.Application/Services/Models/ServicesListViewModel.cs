using System.Collections.Generic;

namespace eGoatDDD.Application.Services.Models
{
    public class ServicesListViewModel
    {
        public ICollection<ServiceDto> Services { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
