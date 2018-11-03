using System;
using System.Collections.Generic;

namespace TheCoopAntiques.Models.ViewModel
{
    public class LayawaysViewModel
    {
        public Layaways Layaways { get; set; }

        public Orders Orders { get; set; }

        public IEnumerable<LayawayPayments> LayawayPayments { get; set; }

        public decimal GetCurrentBalance(DateTime? effectiveDate)
        {
            decimal balance = 0.0M;

            if (LayawayPayments == null) return balance;
            foreach (var payment in LayawayPayments)
            {
                balance = +payment.Amount;
            }
            return balance;
        }
    }
}