using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoopAntiques.Models
{
    public class TransactionTypes
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="Transaction Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Disabled { get; set; }
    }
}
