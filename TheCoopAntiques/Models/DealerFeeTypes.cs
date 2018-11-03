using System.ComponentModel.DataAnnotations;

namespace TheCoopAntiques.Models
{
    public class DealerFeeTypes
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Included")]
        public bool IsDeduction { get; set; }

        public bool Disabled { get; set; }
    }
}