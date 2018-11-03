using System.ComponentModel.DataAnnotations;

namespace TheCoopAntiques.Models
{
    public class Dealers
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Dealer ID")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "PO Box/Apt Num")]
        public string Address2 { get; set; }

        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        public string Zip { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool Disabled { get; set; }

        public override string ToString()
        {
            return $"{Name}: {FirstName} {LastName}";
        }
    }
}