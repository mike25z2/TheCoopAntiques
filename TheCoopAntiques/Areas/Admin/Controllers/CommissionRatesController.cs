using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class CommissionRatesController : ApplicationController
    {
       
        public CommissionRatesController(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.CommissionRates.ToList());
        }

    }
}