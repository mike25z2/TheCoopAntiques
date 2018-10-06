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
    }
}
