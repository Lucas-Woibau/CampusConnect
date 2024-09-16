using CampusConnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CampusConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var instituicaoCounts = _userManager.Users
                .GroupBy(u => u.Instituicao)
                .Select(g => new
                {
                    Instituicao = g.Key,
                    Count = g.Count()
                })
                .ToList();

            ViewBag.UsersInst = JsonConvert.SerializeObject(instituicaoCounts); 
            ViewBag.TotalUsers = instituicaoCounts.Count;

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
