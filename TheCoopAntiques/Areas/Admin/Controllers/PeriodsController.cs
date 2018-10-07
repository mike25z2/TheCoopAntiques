using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Data;
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
            return View(await _db.Periods.OrderBy(p=> p.PeriodName).ToListAsync());
        }
    }
}