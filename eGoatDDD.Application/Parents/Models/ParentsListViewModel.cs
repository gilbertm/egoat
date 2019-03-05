using System.Collections.Generic;

namespace eGoatDDD.Application.Parents.Models
{
    public class ParentsListViewModel
    {
        public IEnumerable<ParentViewModel> Goats { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
