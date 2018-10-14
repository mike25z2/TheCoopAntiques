using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    public class DealersController : ApplicationController
    {
        public DealersController(ApplicationDbContext db): base(db)
        {
           _db = db;
        }

        #region Dealer Info
        public IActionResult Index()
        {
            ViewData["Status"] = StatusVM;
            return View(_db.Dealers.OrderBy(d => d.Name).ToList());
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dealers dealers)
        {
            if (!ModelState.IsValid) return View();
            dealers.Name = dealers.Name.ToUpper();
            _db.Add(dealers);
            await _db.SaveChangesAsync();
            return RedirectToAction("Create", "DealerFees", new {dealerId = dealers.Id});
        }

        //GET EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var dealer = await _db.Dealers.FindAsync(id);
            if (dealer == null) return NotFound();
            return View(dealer);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dealers dealers)
        {
            if (!ModelState.IsValid) return View();
            dealers.Name = dealers.Name.ToUpper();
            _db.Update(dealers);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var dealer = await _db.Dealers.FindAsync(id);
            if (dealer == null) return NotFound();
            return View(dealer);
        }

        // GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var dealers = await _db.Dealers.FindAsync(id);
            return View(dealers);
        }

        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var dealers = await _db.Dealers.FindAsync(id);
            if (dealers == null) return NotFound();
            _db.Remove(dealers);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}