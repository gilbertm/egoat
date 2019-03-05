using System.Collections.Generic;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatsListNonDtoViewModel
    {
        public IList<GoatNonDtoViewModel> Goats { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
