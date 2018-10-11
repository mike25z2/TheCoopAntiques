using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Models;

namespace TheCoopAntiques.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TransactionTypes> TransactionTypes { get; set; }
        public DbSet<Dealers> Dealers { get; set; }
        public DbSet<TaxRates> TaxRates { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Periods> Periods { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<DealerFees> DealerFees { get; set; }
        public DbSet<DealerFeeTypes> DealerFeeTypes { get; set; }
        public DbSet<LayawayPayments> LayawayPayments { get; set; }
        public DbSet<Layaways> Layaways { get; set; }
    }
}
