using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models.ViewModel
{
    public class DealerViewModel
    {
        public Dealers Dealers { get; set; }
        public IEnumerable<DealerFees> DealerFees { get; set; }
    }
}
