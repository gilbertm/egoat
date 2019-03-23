using eGoatDDD.Application.GoatResources.Models;
using System.Collections.Generic;

namespace eGoatDDD.Application.Parents.Models
{
    public class ParentViewModel
    {
        public long ParentId { get; set; }

        public string ColorName { get; set; }
        
        public string Code { get; set; }

        public string Picture { get; set; }

        public char Gender { get; set; }

        public GoatResourceViewModel Resource { get; set; }
    }
}
