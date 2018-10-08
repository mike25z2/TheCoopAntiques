using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models.ViewModel;

namespace TheCoopAntiques.Controllers
{
    public abstract class ApplicationController : Controller
    {
        protected ApplicationDbContext _db;

        public StatusViewModel StatusVM { get; set; }

        public ApplicationController(ApplicationDbContext db)
        {
            _db = db;
            StatusVM = new StatusViewModel()
            {
                CurrentPeriod = _db.Periods.FirstOrDefault(p => p.IsCurrent == true)
            };
        }
    }
}