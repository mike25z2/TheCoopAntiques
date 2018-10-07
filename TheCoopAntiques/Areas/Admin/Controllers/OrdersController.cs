using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models.ViewModel;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OrdersViewModel OrdersVM { get; set; }
        
        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
            OrdersVM = new OrdersViewModel()
            {
                TransactionTypes = _db.TransactionTypes.Where(t=>t.Disabled==false).ToList(),
                Dealers = _db.Dealers.Where(d=>d.Disabled==false).OrderBy(d => d.Name).ToList(),
                Orders = new Models.Orders(),
                Items = _db.Items.Where(i => i.OrderId == OrdersVM.Orders.Id)
            };
        }

        public async Task<IActionResult> Index()
        {
            var orders = _db.Orders.Include(m => m.TransactionTypes).Include(m => m.Dealers);
            return View(await orders.ToListAsync());
        }

        // GET CREATE
        public IActionResult Create()
        {
            OrdersVM.Orders.DealerId =  _db.Dealers.First(d => d.Name == "None").Id;
            return View(OrdersVM);
        }

        // POST CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid) return View(OrdersVM);
            OrdersVM.Orders.InvoiceNumber= OrdersVM.Orders.InvoiceNumber.PadLeft(8, '0');
            _db.Orders.Add(OrdersVM.Orders);
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit", new {id = OrdersVM.Orders.Id});
        }

        //GET Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            OrdersVM.Orders = await _db.Orders.Include(m => m.TransactionTypes).Include(m => m.Dealers)
                .SingleOrDefaultAsync(m => m.Id == id);
            ViewBag.Items = OrdersVM.Items;
            if (OrdersVM.Orders == null) return NotFound();
            return View(OrdersVM);
        }

        //GET EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            OrdersVM.Orders = await _db.Orders.Include(m => m.TransactionTypes).Include(m => m.Dealers)
                .SingleOrDefaultAsync(m => m.Id == id);
            ViewBag.Items = OrdersVM.Items;
            if (OrdersVM.Orders == null) return NotFound();

            return View(OrdersVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid) return View();
            var OrderFromDb = _db.Orders.FirstOrDefault(m => m.Id == OrdersVM.Orders.Id);
            if (OrderFromDb == null) return View();
            OrderFromDb.InvoiceNumber = OrdersVM.Orders.InvoiceNumber.PadLeft(8, '0');
            OrderFromDb.OrderDate = OrdersVM.Orders.OrderDate;
            OrderFromDb.TransactionTypeId = OrdersVM.Orders.TransactionTypeId;
            OrderFromDb.DealerId = OrdersVM.Orders.DealerId;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            OrdersVM.Orders = await _db.Orders.Include(m => m.TransactionTypes).Include(m => m.Dealers)
                .SingleOrDefaultAsync(m => m.Id == id);
            ViewBag.Items = OrdersVM.Items;
            if (OrdersVM.Orders == null) return NotFound();
            return View(OrdersVM);
        }

    }
}