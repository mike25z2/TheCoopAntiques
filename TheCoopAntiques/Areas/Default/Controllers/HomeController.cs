using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheCoopAntiques.Data;
using TheCoopAntiques.Models;

namespace TheCoopAntiques.Controllers
{
    [Area("Default")]
    public class HomeController : ApplicationController
    {
        public HomeController(ApplicationDbContext db) : base(db)
        {
        }

        public IActionResult Index()
        {
            ViewData["Status"] = StatusVM;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}