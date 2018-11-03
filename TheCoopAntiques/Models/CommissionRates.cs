using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class CommissionRates
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
