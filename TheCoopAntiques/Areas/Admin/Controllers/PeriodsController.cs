using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class PeriodsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PeriodsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Count = _db.Periods.Count(p => p.IsCurrent == true);
            return View(await _db.Periods.OrderBy(p => p.NumericPeriod).ToListAsync());
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreate(Periods periods)
        {
            if (!ModelState.IsValid) return View();
            if (periods.PeriodType == "M")
            {
                periods.MonthInt = periods.StartDate.Month;
                periods.YearInt = periods.EndDate.Year;
            }

            _db.Add(periods);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET SET PERIOD
        public async Task<IActionResult> SetPeriod(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            var periodsList = await _db.Periods.Where(p => p.IsCurrent == true).ToListAsync();
            if (periodsList != null && periodsList.Count > 0)
            {
                foreach (Periods p in periodsList)
                {
                    p.IsCurrent = false;
                    _db.Update(p);
                }
                await _db.SaveChangesAsync();
            }

            var periodToActivate = await _db.Periods.FindAsync(id);
            periodToActivate.IsCurrent = true;
            _db.Update(periodToActivate);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}