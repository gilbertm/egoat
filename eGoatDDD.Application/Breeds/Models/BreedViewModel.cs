namespace eGoatDDD.Application.Breeds.Models
{
    public class BreedViewModel
    {
        public BreedDto Breed { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
