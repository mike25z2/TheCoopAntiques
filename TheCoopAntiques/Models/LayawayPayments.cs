﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCoopAntiques.Models
{
    public class LayawayPayments
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int LayAwayId { get; set; }

        [ForeignKey("LayAwayId")]
        public virtual Layaways Layaways { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Display(Name = "Transaction Type")]
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        [Display(Name = "Type")]
        public virtual TransactionTypes TransactionTypes { get; set; }

        public bool Void { get; set; }
    }
}