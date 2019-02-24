using System.Collections.Generic;

namespace eGoatDDD.Application.Breeds.Models
{
    public class BreedsListViewModel
    {
        public IEnumerable<BreedDto> Breeds { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
