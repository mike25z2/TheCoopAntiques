using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;

namespace TheCoopAntiques.Utility
{
    public class DealerProcessor
    {
        private const decimal CREDIT_FEE = 0.03M;
        private const decimal COOP_FEE = 0.10M;
        private readonly ApplicationDbContext _db;
        private readonly Periods _period;
        private bool _lock;

        public DealerProcessor(Periods period, ApplicationDbContext db)
        {
            _db = db;
            _period = period;
        }

        public Dealers Dealer { get; set; }

        public List<Items> Items => _db.Items.Include(i=> i.Orders).Include(i=> i.Orders.TransactionTypes)
                            .Where(i => i.DealerId == Dealer.Id)
                            .Where(i => i.Orders.OrderDate >= _period.StartDate)
                            .Where(i => i.Orders.OrderDate <= _period.EndDate).ToList();

        public List<DealerFees> DealerFees => _db.DealerFees.Where(df => df.DealerId == Dealer.Id)
                                .Where(df => df.PeriodId == _period.Id).ToList();

        public List<Items> OnAccountItems => _db.Items.Include(i=> i.Orders).Include(i=> i.Orders.TransactionTypes)
                                            .Where(i=> i.Orders.TransactionTypes.Name == "ON ACCOUNT")
                                            .Where(i=> i.Orders.DealerId== Dealer.Id)
                                            .Where(i => i.Orders.OrderDate >= _period.StartDate)
                                            .Where(i => i.Orders.OrderDate <= _period.EndDate).ToList();

        public bool Lock
        {
            get
            {
                if (DealerFees.Count == 0) return true;
                return _lock;
            }
            set => _lock = value;
        }

        public decimal SalesTotal => Items.Sum(i => i.Amount);

        public decimal TaxSales
        {
            get
            {
                try
                {
                    return Items.Where(i => i.TaxExempt == false).Sum(i => i.Amount);
                }
                catch (Exception e)
                {
                    return 0M;
                }

            }
        }
        
        public decimal Tax
        {
            get
            {
                try
                {
                    return Items.Where(i => !i.TaxExempt).Sum( i => (i.Amount*i.Orders.TaxRate));
                }
                catch (Exception e)
                {
                    return 0M;
                }
            }
        }
           
        public decimal CreditSales => Items.Where(i => i.Orders.TransactionTypes.Name == "CREDIT").Sum(i => i.Amount);

        public decimal CashSales => Items.Where(i => i.Orders.TransactionTypes.Name == "CASH").Sum(i => i.Amount);

        public decimal FeesTotal
        {
            get
            {
                if (Lock)
                {
                    return 0M;
                }
                try
                {
                    return DealerFees.Sum(df => df.Amount);
                }
                catch (Exception e)
                {
                    return 0M;
                }
            }
        }

        public decimal OnAccountTotal
        {
            get
            {
                if (OnAccountItems.Count > 0)
                {
                    return OnAccountItems.Sum(i => i.Amount) +
                           OnAccountItems.Where(i => !i.TaxExempt).Sum(i => i.Amount * i.Orders.TaxRate);
                }

                return 0M;
            }
        }

        public decimal AmountToBePaid => SalesTotal - (SalesTotal * COOP_FEE) - (CreditSales * CREDIT_FEE)
                                         - (FeesTotal) - (OnAccountTotal);

        public decimal Balance => _db.DealerPayments.Where(b=>b.PaymentDate<=DateTime.Today).Sum(b => b.Amount);
    }
}
