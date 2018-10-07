using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        private ApplicationUserViewModel ApplicationUserVM { get; set; }

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUserVM = new ApplicationUserViewModel()
            {
                ApplicationUsers = new Models.ApplicationUser(),
                Dealers = _db.Dealers.Where(d => d.Disabled == false).ToList()
            };
        }

        public IActionResult Index()
        {
            var applicationUsers = _db.ApplicationUser.Include(d=>d.Dealers).DefaultIfEmpty().ToList();
            return View(applicationUsers);
        }
    }
}