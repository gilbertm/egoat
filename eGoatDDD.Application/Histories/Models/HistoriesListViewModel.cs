using eGoatDDD.Application.Services.Models;
using System.Collections.Generic;

namespace eGoatDDD.Application.Histories.Models
{
    public class HistoriesListViewModel
    {
        public ICollection<ServiceDto> Histories { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
