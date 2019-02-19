using System.Collections.Generic;

namespace eGoatDDD.Application.Ledgers.Models
{
    public class LedgersListViewModel
    {
        public IEnumerable<LedgerDto> Ledgers { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
