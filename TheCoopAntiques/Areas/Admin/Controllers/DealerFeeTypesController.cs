using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class DealerFeeTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DealerFeeTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.DealerFeeTypes.ToList());
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DealerFeeTypes dealerFeeTypes)
        {
            if (!ModelState.IsValid) return View();
            _db.Add(dealerFeeTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var dealerFeeTypes = await _db.DealerFeeTypes.FindAsync(id);
            if (dealerFeeTypes == null) return NotFound();
            return View(dealerFeeTypes);
        }

        //POST EDIT
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(int? id, DealerFeeTypes dealerFeeTypes )
        {
            if (id != dealerFeeTypes.Id) return NotFound();
            if (!ModelState.IsValid) return View(dealerFeeTypes);
            _db.Update(dealerFeeTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}