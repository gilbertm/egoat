using System.Collections.Generic;

namespace eGoatDDD.Application.Colors.Models
{
    public class ColorsListViewModel
    {
        public IEnumerable<ColorDto> Colors { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
