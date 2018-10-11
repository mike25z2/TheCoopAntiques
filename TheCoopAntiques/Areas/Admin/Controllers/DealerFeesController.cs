using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Dependency;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;
using TheCoopAntiques.Data.Migrations;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class DealerFeesController : ApplicationController
    {
        [BindProperty]
        public DealerFeesViewModel DealerFeesVM { get; set; }

        public DealerViewModel DealersVM { get; set; }

        public DealerFeesController(ApplicationDbContext db) : base(db)
        {
            DealerFeesVM = new DealerFeesViewModel()
            {
                DealerFees = new Models.DealerFees(),
                DealerFeeTypes = _db.DealerFeeTypes.Where(df => df.Disabled == false).ToList(),
                Periods = _db.Periods.ToList()
            };
            DealersVM = new DealerViewModel()
            {
                Dealers = new Models.Dealers(),
                DealerFees = _db.DealerFees.Include(m => m.DealerFeeTypes).Include(m => m.Periods).Where(m => m.DealerId == DealersVM.Dealers.Id)
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
            return View(DealerFeesVM);
        }

        //POST CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int dealerId)
        {
            if (!ModelState.IsValid) return View(DealerFeesVM);
            DealerFeesVM.DealerFees.DealerId = dealerId;
            _db.DealerFees.Add(DealerFeesVM.DealerFees);
            await _db.SaveChangesAsync();

            return RedirectToAction("Create", "DealerFees", new {dealerId});
        }
    }
}