using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCoopAntiques.Models
{
    public class DealerFees
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DealerId { get; set; }

        [ForeignKey("DealerId")]
        [Display(Name = "Dealer Id")]
        public virtual Dealers Dealers { get; set; }

        [Required]
        [Display(Name = "Period")]
        public int PeriodId { get; set; }

        [ForeignKey("PeriodId")]
        public virtual Periods Periods { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Fee Type")]
        public int DealerFeeTypeId { get; set; }

        [ForeignKey("DealerFeeTypeId")]
        public virtual DealerFeeTypes DealerFeeTypes { get; set; }
    }
}