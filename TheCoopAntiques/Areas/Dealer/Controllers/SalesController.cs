using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TheCoopAntiques.Data;
using TheCoopAntiques.Extensions;
using TheCoopAntiques.Models;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner + "," + SD.Volunteer + "," + SD.DealerV)]
    [Area("Dealer")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SalesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value);
            var items = _db.Items.Include(i => i.Orders.TransactionTypes)
                .Where(i => i.DealerId == applicationUser.DealerId).ToList();
            return View(items);
        }
    }
}


