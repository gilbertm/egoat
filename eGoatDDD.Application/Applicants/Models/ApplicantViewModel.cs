namespace eGoatDDD.Application.Applicants.Models
{
    public class ApplicantViewModel
    {
        public ApplicantDto Applicant { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
