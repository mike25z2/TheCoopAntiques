using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ApplicationUserViewModel ApplicationUserVM { get; set; }

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
            var applicationUsers = _db.ApplicationUser.Include(d => d.Dealers).DefaultIfEmpty().ToList();
            return View(applicationUsers);
        }

        //GET EDIT
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0) return NotFound();

            ApplicationUserVM.ApplicationUsers = await _db.ApplicationUser.Include(m => m.Dealers).SingleOrDefaultAsync(m => m.Id == id);
            if (ApplicationUserVM.ApplicationUsers == null) return NotFound();
            return View(ApplicationUserVM);
        }

        //POST EDIT
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult POSTEdit(string id)
        {
            if (id != ApplicationUserVM.ApplicationUsers.Id) return NotFound();
            if (!ModelState.IsValid) return View(ApplicationUserVM);
            ApplicationUser userFromDb =
                _db.ApplicationUser.Include(m => m.Dealers).FirstOrDefault(u => u.Id == id);
            if (userFromDb == null) return NotFound();
            userFromDb.FirstName = ApplicationUserVM.ApplicationUsers.FirstName;
            userFromDb.LastName = ApplicationUserVM.ApplicationUsers.LastName;
            userFromDb.PhoneNumber = ApplicationUserVM.ApplicationUsers.PhoneNumber;
            userFromDb.DealerId = ApplicationUserVM.ApplicationUsers.DealerId;

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //GET DELETE
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0) return NotFound();

            ApplicationUserVM.ApplicationUsers = await _db.ApplicationUser.Include(m => m.Dealers).SingleOrDefaultAsync(m => m.Id == id);
            if (ApplicationUserVM.ApplicationUsers == null) return NotFound();
            return View(ApplicationUserVM);
        }

        //POST EDIT
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult POSTDelete(string id)
        {
            if (id != ApplicationUserVM.ApplicationUsers.Id) return NotFound();
            if (!ModelState.IsValid) return View(ApplicationUserVM);
            ApplicationUser userFromDb =
                _db.ApplicationUser.Include(m => m.Dealers).FirstOrDefault(u => u.Id == id);
            if (userFromDb == null) return NotFound();
            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}