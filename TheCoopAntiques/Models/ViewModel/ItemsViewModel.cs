using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models.ViewModel
{
    public class ItemsViewModel
    {
        public Items Items { get; set; }
        public IEnumerable<Dealers> Dealers { get; set; }
    }
}
