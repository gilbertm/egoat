namespace eGoatDDD.Application.Goats.Models
{
    public class GoatViewModel
    {
        public GoatDto Goat { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
