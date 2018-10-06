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
    public class TransactionTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TransactionTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.TransactionTypes.ToList());
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionTypes transactionTypes)
        {
            if (!ModelState.IsValid) return View();
            transactionTypes.Name = transactionTypes.Name.ToUpper();
            _db.Add(transactionTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var transactionType = await _db.TransactionTypes.FindAsync(id);
            return View(transactionType);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransactionTypes transactionTypes)
        {
            if (id != transactionTypes.Id) return NotFound();
            if (!ModelState.IsValid) return View(transactionTypes);
            transactionTypes.Name = transactionTypes.Name.ToUpper();
            _db.Update(transactionTypes);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var transactionType = await _db.TransactionTypes.FindAsync(id);
            return View(transactionType);
        }

        // GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var transactionType = await _db.TransactionTypes.FindAsync(id);
            return View(transactionType);
        }

        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var transactionTypes = await _db.TransactionTypes.FindAsync(id);
            if (transactionTypes == null) return NotFound();
            _db.Remove(transactionTypes);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}