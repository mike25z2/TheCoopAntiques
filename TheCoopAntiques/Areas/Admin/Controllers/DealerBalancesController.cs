using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class DealerBalancesController : ApplicationController
    {
        public DealerViewModel DealersVM { get; set; }

        public DealerBalancesController(ApplicationDbContext db) : base(db)
        {
            _db = db;
            DealersVM = new DealerViewModel()
            {
                Dealers = new Models.Dealers(),
                DealerPayments = _db.DealerPayments.Where(m => m.DealerId == DealersVM.Dealers.Id)
            };
        }

        //INDEX: Passing dealer ID
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Dealers");
            DealersVM.Dealers = await _db.Dealers.Where(d => d.Id == id).FirstOrDefaultAsync();
            return View(DealersVM);
        }

        //GET CREATE
        public async Task<IActionResult> Create(int? dealerId)
        {
            if (dealerId == null) return NotFound();
            DealersVM.Dealers = await _db.Dealers.SingleOrDefaultAsync(m => m.Id == dealerId);
            ViewBag.DealersVM = DealersVM;
            //set defaults
            return View();
        }
    }
}