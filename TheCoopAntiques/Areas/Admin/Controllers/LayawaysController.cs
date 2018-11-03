using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class LayawaysController : ApplicationController
    {
        [BindProperty]
        public LayawaysViewModel LayawaysVM { get; set; }

        public LayawaysController(ApplicationDbContext db) : base(db)
        {
            _db = db;
            LayawaysVM = new LayawaysViewModel()
            {
                Layaways = new Models.Layaways(),
                LayawayPayments = _db.LayawayPayments.Where(lay => lay.LayAwayId == LayawaysVM.Layaways.Id).ToList(),
                Orders = new Models.Orders()
            };
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Status"] = StatusVM;
            var layaways = _db.Layaways.Include(m => m.Orders);
            return View(await layaways.ToListAsync());
        }

        //GET CREATE
        public IActionResult Create(int OrderId)
        {
            return View();
        }
    }
}