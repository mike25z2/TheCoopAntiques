using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ItemsViewModel ItemsVM { get; set; }

        public OrdersViewModel OrdersVM { get; set; }

        public ItemsController(ApplicationDbContext db)
        {
            _db = db;
            ItemsVM = new ItemsViewModel()
            {
                Dealers = _db.Dealers.Where(d => d.Disabled==false).Where(d=> d.Id !=6).OrderBy(d=>d.Name).ToList(),
                Items = new Models.Items()
            };
            OrdersVM = new OrdersViewModel()
            {
                TransactionTypes = _db.TransactionTypes.Where(t => t.Disabled == false).ToList(),
                Dealers = _db.Dealers.Where(d => d.Disabled == false).OrderBy(d => d.Name).ToList(),
                Orders = new Models.Orders(),
                Items = _db.Items.Where(i => i.OrderId == OrdersVM.Orders.Id).Include(i=>i.Dealers)
            };
        }

        public async Task<IActionResult> Index()
        {
            var items = _db.Items.Include(m => m.Dealers);
            return View(await items.ToListAsync());
        }

        //GET CREATE
        public async Task<IActionResult> Create(int orderId)
        {
            Orders GetOrder = _db.Orders.Single(o => o.Id == orderId);
            if (GetOrder == null) return NotFound();
            ViewBag.Order= GetOrder;
            OrdersVM.Orders =  await _db.Orders.Include(m => m.TransactionTypes).Include(m => m.Dealers)
                .SingleOrDefaultAsync(m => m.Id == orderId);
            ViewBag.OrdersVM = OrdersVM;
            return View(ItemsVM);
        }

        //POST CREATE
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(int orderId)
        {
            if (!ModelState.IsValid) return View(ItemsVM);
            ItemsVM.Items.OrderId = orderId;
            _db.Items.Add(ItemsVM.Items);
            await _db.SaveChangesAsync();

            return RedirectToAction("Create", "Items", new {orderId});
        }

        //GET EDIT
        //id is coming in as itemId
        public async Task<IActionResult> Edit(int? orderId, int? id, string viewId, string controllerId)
        {
            if (orderId == null) return NotFound();
            Orders GetOrder = _db.Orders.Single(o => o.Id == orderId);
            if (GetOrder == null) return NotFound();
            ViewBag.Order = GetOrder;
            ItemsVM.Items = await _db.Items.Include(i => i.Dealers).SingleOrDefaultAsync(i => i.Id == id);
            OrdersVM.Orders = await _db.Orders.Include(m => m.TransactionTypes).Include(m => m.Dealers)
                .SingleOrDefaultAsync(m => m.Id == orderId);
            ViewBag.OrdersVM = OrdersVM;
            return View(ItemsVM);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int orderId)
        {
            if (!ModelState.IsValid) return View(ItemsVM);
            var itemFromDb = await _db.Items.FirstOrDefaultAsync(m => m.Id == ItemsVM.Items.Id);
            if (itemFromDb == null) return View(ItemsVM);
            itemFromDb.DealerId = ItemsVM.Items.DealerId;
            itemFromDb.Description = ItemsVM.Items.Description;
            itemFromDb.Amount = ItemsVM.Items.Amount;
            itemFromDb.TaxExempt = ItemsVM.Items.TaxExempt;
            await _db.SaveChangesAsync();

            return RedirectToAction("Edit", "Orders", new {id = orderId});
        }

        //GET DELETE
        //id is coming in as Item.Id
        public async Task<IActionResult> Delete(int id, string viewId, string controllerId)
        {
            var items = await _db.Items.FindAsync(id);
            if (items == null) return NotFound();
            _db.Remove(items);
            await _db.SaveChangesAsync();

            if (controllerId == "Orders") return RedirectToAction(viewId, controllerId, new { id = items.OrderId });
            return RedirectToAction(viewId, controllerId, new { orderId = items.OrderId });
        }
        
    }
}