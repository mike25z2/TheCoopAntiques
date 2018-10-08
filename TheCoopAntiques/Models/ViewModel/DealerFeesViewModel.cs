using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models.ViewModel
{
    public class DealerFeesViewModel
    {
        public Dealers Dealers { get; set; }
        public DealerFees DealerFees { get; set; }
        public IEnumerable<DealerFeeTypes> DealerFeeTypes { get; set; }
        public Periods Periods { get; set; }
    }
}
