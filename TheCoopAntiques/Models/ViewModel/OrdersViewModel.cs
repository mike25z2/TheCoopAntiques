﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models.ViewModel
{
    public class OrdersViewModel
    {
        public Orders Orders { get; set; }

        [Display(Name="Type")]
        public IEnumerable<TransactionTypes> TransactionTypes { get; set; }

        [Display(Name="On Account")]
        public IEnumerable<Dealers> Dealers { get; set; }

        public IEnumerable<Items> Items { get; set; }

    }
}
