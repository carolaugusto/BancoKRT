using Microsoft.AspNetCore.Mvc;
using ProjetoBancoKRT.Models;
using System.Diagnostics;

namespace ProjetoBancoKRT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
