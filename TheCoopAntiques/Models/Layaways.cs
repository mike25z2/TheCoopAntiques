using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class Layaways
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Orders Orders { get; set; }

        [Display(Name="Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public decimal Deposit { get; set; }

        public string Notes { get; set; }



    }
}
