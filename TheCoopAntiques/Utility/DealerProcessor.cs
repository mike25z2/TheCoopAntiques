using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;

namespace TheCoopAntiques.Utility
{
    public class DealerProcessor
    {
        private readonly decimal CREDIT_FEE;
        private readonly decimal COOP_FEE;
        private readonly ApplicationDbContext _db;
        private readonly Periods _period;
        private bool _lock;

        public DealerProcessor(Periods period, ApplicationDbContext db)
        {
            _db = db;
            _period = period;
            CREDIT_FEE = _db.CreditFees
                .Where(c => c.StartDate <= DateTime.Today).First(c => c.EndDate >= DateTime.Today).Amount;
            COOP_FEE = _db.CommissionRates
                .Where(c => c.StartDate <= DateTime.Today).First(c => c.EndDate >= DateTime.Today).Amount;
        }

        public Dealers Dealer { get; set; }

        public List<Items> Items => _db.Items.Include(i => i.Orders).Include(i => i.Orders.TransactionTypes)
                            .Where(i => i.DealerId == Dealer.Id)
                            .Where(i => i.Orders.OrderDate >= _period.StartDate)
                            .Where(i => i.Orders.OrderDate <= _period.EndDate).ToList();

        public List<DealerFees> DealerFees => _db.DealerFees.Where(df => df.DealerId == Dealer.Id)
                                .Where(df => df.PeriodId == _period.Id).ToList();

        public List<Items> OnAccountItems => _db.Items.Include(i => i.Orders).Include(i => i.Orders.TransactionTypes)
                                            .Where(i => i.Orders.TransactionTypes.Name == "ON ACCOUNT")
                                            .Where(i => i.Orders.DealerId == Dealer.Id)
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
                catch (Exception ex)
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
                    return Items.Where(i => !i.TaxExempt).Sum(i => (i.Amount * i.Orders.TaxRate));
                }
                catch (Exception ex)
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

        public decimal AmountToBePaid => SalesTotal - Commission - CreditFee
                                         - (FeesTotal) - (OnAccountTotal);

        public decimal Balance => _db.DealerPayments.Where(b => b.EntryDate <= DateTime.Today).Where(b=>!b.Void).Sum(b => b.Amount);

        public decimal Commission => SalesTotal * COOP_FEE;

        public decimal CreditFee => CreditSales * CREDIT_FEE;
    }
}