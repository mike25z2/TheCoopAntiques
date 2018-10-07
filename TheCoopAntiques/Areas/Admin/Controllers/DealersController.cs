using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;


namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DealersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DealersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Dealers.OrderBy(d=>d.Name).ToList());
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
            _db.Add(dealers);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}