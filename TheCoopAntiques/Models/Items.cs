using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TheCoopAntiques.Models
{
    public class Items
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Display(Name="Tax Exempt")]
        public bool TaxExempt { get; set; }

        [Required]
        [Display(Name="Dealer")]
        public int DealerId { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealers Dealers { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Orders Orders { get; set; }
    }
}
