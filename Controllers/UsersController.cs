using CampusConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CampusConnect.Controllers
{
    //[Authorize(Roles = "admin")]
    //[Route("/Admin/[controller]/{action}/{id}")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly int _pageSize = 10;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(int? pageIndex)
        {
            IQueryable<ApplicationUser> query = _userManager.Users.OrderByDescending(o => o.Nome);

            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            decimal count = query.Count();
            int totalPages = (int)Math.Ceiling(count / _pageSize);
            query = query.Skip(((int)pageIndex - 1) * _pageSize).Take(_pageSize);

            var users = query.ToList();

            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalPages = totalPages;

            return View(users);
        }
    }
}
