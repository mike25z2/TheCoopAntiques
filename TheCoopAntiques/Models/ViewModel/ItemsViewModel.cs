using System.Collections.Generic;

namespace TheCoopAntiques.Models.ViewModel
{
    public class ItemsViewModel
    {
        public Items Items { get; set; }
        public IEnumerable<Dealers> Dealers { get; set; }
    }
}