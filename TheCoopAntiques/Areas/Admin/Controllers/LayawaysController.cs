using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;
using TheCoopAntiques.Models.ViewModel;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class LayawaysController : ApplicationController
    {
        public LayawaysViewModel LayawaysVM { get; set; }

        public LayawaysController(ApplicationDbContext db): base(db)
        {
            _db = db;
            LayawaysVM = new LayawaysViewModel()
            {
                Layaways = new Layaways(),
                LayawayPayments = _db.LayawayPayments.Where(lay => lay.LayAwayId == LayawaysVM.Layaways.Id).ToList(),
                OrdersVM = new OrdersViewModel()
                {
                    TransactionTypes = _db.TransactionTypes.Where(t => t.Disabled == false).ToList(),
                    Dealers = _db.Dealers.Where(d => d.Disabled == false).OrderBy(d => d.Name).ToList(),
                    Orders = new Models.Orders(),
                    Items = _db.Items.Where(i => i.OrderId == LayawaysVM.OrdersVM.Orders.Id)
                },
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET CREATE
        public IActionResult Create(int OrderId)
        {
            return View();
        }
    }
}