using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Data;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
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