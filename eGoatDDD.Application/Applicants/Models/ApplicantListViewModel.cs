using System.Collections.Generic;

namespace eGoatDDD.Application.Applicants.Models
{
    public class ApplicantListViewModel
    {
        public IEnumerable<ApplicantDto> Applicants { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
