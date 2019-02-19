namespace eGoatDDD.Application.Categories.Models
{
    public class CategoryViewModel
    {
        public CategoryDto Category { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
