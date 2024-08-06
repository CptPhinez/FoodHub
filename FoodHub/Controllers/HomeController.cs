using FoodHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
            {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult store()
            {
               
            return View();
            }
        public ActionResult store2()
        {

            return View();
        }
        public ActionResult store3()
        {

            return View();
        }
        public ActionResult store4()
        {

            return View();
        }
        public ActionResult store5()
        {

            return View();
        }
        public ActionResult store6()
        {

            return View();
        }
        public ActionResult Order()
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
