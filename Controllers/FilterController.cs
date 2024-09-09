using CampusConnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CampusConnect.Controllers
{
    public class FilterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public FilterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(bool? rota)
        {

            return View();
        }
    }
}
