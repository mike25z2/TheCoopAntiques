using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TheCoopAntiques.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name="Dealer Id")]
        public int DealerId { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealers Dealers { get; set; }

        [NotMapped]
        public string AccessLevel { get; set; }
    }
}
