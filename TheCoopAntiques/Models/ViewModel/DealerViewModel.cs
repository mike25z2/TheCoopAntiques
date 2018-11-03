using System.Collections.Generic;

namespace TheCoopAntiques.Models.ViewModel
{
    public class DealerViewModel
    {
        public Dealers Dealers { get; set; }
        public IEnumerable<DealerFees> DealerFees { get; set; }
        public IEnumerable<DealerPayments> DealerPayments { get; set; }
    }
}