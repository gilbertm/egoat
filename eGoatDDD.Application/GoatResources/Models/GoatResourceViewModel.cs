using System;

namespace eGoatDDD.Application.GoatResources.Models
{
    public class GoatResourceViewModel
    {
        public Guid ResourceId { get; set; }

        public string Filename { get; set; }

        public string Location { get; set; }
    }
}
