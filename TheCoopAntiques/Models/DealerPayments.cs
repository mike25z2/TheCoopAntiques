using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class DealerPayments
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dealer")]
        public int DealerId { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealers Dealers { get; set; }

        [Required]
        [Display(Name = " Payment Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string Description { get; set; }

        public string Notes { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }
        
        public bool Void { get; set; }
    }
}
