using System.Collections.Generic;

namespace eGoatDDD.Application.Loans.Models
{
    public class LoansListViewModel
    {
        public IEnumerable<LoanDto> Loans { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
