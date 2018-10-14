using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCoopAntiques.Controllers;
using TheCoopAntiques.Data;
using TheCoopAntiques.Utility;

namespace TheCoopAntiques.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser + "," + SD.Owner)]
    [Area("Admin")]
    public class MonthlyProcessingController : ApplicationController
    {
        public DealerProcessor DealerProcessor { get; set; }
        public List<DealerProcessor> ProcessingList { get; set; }

        public MonthlyProcessingController(ApplicationDbContext db) : base(db)
        {
            _db = db;
            ProcessingList = new List<DealerProcessor>();
        }

        public async Task<IActionResult> Index()
        {
            var dealers = await _db.Dealers.Where(d => d.Disabled == false).ToListAsync();
            foreach (var d in dealers)
            {
                DealerProcessor = new DealerProcessor(StatusVM.CurrentPeriod, _db)
                {
                    Dealer = d
                };
                ProcessingList.Add(DealerProcessor);
            }

            ViewData["Status"] = StatusVM;
            return View(ProcessingList);
        }
    }


}