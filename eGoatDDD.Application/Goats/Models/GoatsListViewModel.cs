using System.Collections.Generic;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatsListViewModel
    {
        public IEnumerable<GoatDto> Goats { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
