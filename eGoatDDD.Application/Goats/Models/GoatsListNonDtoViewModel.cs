using X.PagedList;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatsListNonDtoViewModel
    {
        public IPagedList<GoatViewModel> Goats { get; set; }

        public int TotalPages { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
