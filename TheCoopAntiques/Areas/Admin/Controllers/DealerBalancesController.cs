using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    public class DealerBalancesController : ApplicationController
    {
        public DealerBalancesController(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}