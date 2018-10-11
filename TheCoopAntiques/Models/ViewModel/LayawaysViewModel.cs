using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models.ViewModel
{
    public class LayawaysViewModel
    {
        public Layaways Layaways { get; set; }

        public OrdersViewModel OrdersVM { get; set; }

        public IEnumerable<LayawayPayments> LayawayPayments { get; set; }

        public decimal GetCurrentBalance(DateTime? effectiveDate)
        {
            decimal balance = 0.0M;

            foreach (var payment in LayawayPayments)
            {
                balance = +payment.Amount;
            }

            return balance;
        }
    }
}
