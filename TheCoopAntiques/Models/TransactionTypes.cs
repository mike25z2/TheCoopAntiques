using System.ComponentModel.DataAnnotations;

namespace TheCoopAntiques.Models
{
    public class TransactionTypes
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Transaction Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Disabled { get; set; }
    }
}