using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TheCoopAntiques.Models.ViewModel
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser ApplicationUsers { get; set; }
        public IEnumerable<Dealers> Dealers { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}