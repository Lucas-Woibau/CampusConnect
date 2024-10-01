using CampusConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CampusConnect.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("/Admin/[controller]/{action=Index}")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly int _pageSize = 25;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string? id, int? pageIndex, string? search, string? rota, string? cidade, string? instituicao)
        {
            IQueryable<ApplicationUser> query = _userManager.Users;

            if (search != null)
            {
                query = query.Where(p => p.Nome.Contains(search) || p.Sobrenome.Contains(search));
            }

            if (rota != null && rota.Length > 0)
            {
                query = query.Where(p => p.Rota.Contains(rota));
            }

            if (cidade != null && cidade.Length > 0)
            {
                query = query.Where(p => p.Cidade.Contains(cidade));
            }

            if (instituicao != null && instituicao.Length > 0)
            {
                query = query.Where(p => p.Instituicao.Contains(instituicao));
            }

            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            decimal count = query.Count();
            int totalPages = (int)Math.Ceiling(count / _pageSize);
            query = query.Skip(((int)pageIndex - 1) * _pageSize).Take(_pageSize);

            var users = query.ToList();

            ViewBag.Users = users;

            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = string.IsNullOrEmpty(search) ? "" : search;

            var SearchUsers = new SearchUsers()
            {
                Search = search,
                Rota = rota,
                Cidade = cidade,
                Instituicao = instituicao,
            };

            var todasInstituicoes = _userManager.Users.Select(u => u.Instituicao).Distinct().ToList();
            var todasAsRotas = _userManager.Users.Select(u => u.Rota).Distinct().ToList();
            var todasAsCidades = _userManager.Users.Select(u => u.Cidade).Distinct().ToList();

            ViewData["Instituicoes"] = todasInstituicoes;
            ViewData["Rotas"] = todasAsRotas;
            ViewData["Cidades"] = todasAsCidades;

            var allRoles = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
                .Distinct()
                .ToList();
            ViewBag.AllRoles = allRoles;

            var userRoles = new Dictionary<string, string>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.FirstOrDefault() ?? "Nenhuma classe atribuída";
            }
            ViewBag.UserRoles = userRoles;
            var availableRoles = _roleManager.Roles.ToList();

            var items = new List<SelectListItem>();
            foreach (var role in availableRoles)
            {
                items.Add(new SelectListItem { Text = role.NormalizedName, Value = role.Name });
            }

            ViewBag.SelectItems = items;

            return View(SearchUsers);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string? id, string? newRole)
        {
            if (id == null || newRole == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Id == appUser.Id)
            {
                return RedirectToAction("Index", "Users");
            }

            var roleExists = await _roleManager.RoleExistsAsync(newRole);
            if (!roleExists)
            {
                return RedirectToAction("Index", "Users", new { id });
            }

            var userRoles = await _userManager.GetRolesAsync(appUser);
            await _userManager.RemoveFromRolesAsync(appUser, userRoles);
            var result = await _userManager.AddToRoleAsync(appUser, newRole);

            if (result.Succeeded)
            {
                //TempData["SuccessMessage"] = "Role alterada com sucesso.";
            }
            else
            {
                //TempData["ErrorMessage"] = "Ocorreu um erro ao alterar a role.";
            }

            return RedirectToAction("Index", "Users", new { id });
        }

        public IActionResult Call(string? rota, string? cidade, string? instituicao)
        {
            IQueryable<ApplicationUser> query = _userManager.Users;

            if (rota != null && rota.Length > 0)
            {
                query = query.Where(p => p.Rota.Contains(rota));
            }

            if (cidade != null && cidade.Length > 0)
            {
                query = query.Where(p => p.Cidade.Contains(cidade));
            }

            if (instituicao != null && instituicao.Length > 0)
            {
                query = query.Where(p => p.Instituicao.Contains(instituicao));
            }

            var users = query.ToList();

            ViewBag.Users = users;

            var SearchUsers = new SearchUsers()
            {
                Rota = rota,
                Cidade = cidade,
                Instituicao = instituicao,
            };

            var todasInstituicoes = _userManager.Users.Select(u => u.Instituicao).Distinct().ToList();
            var todasAsRotas = _userManager.Users.Select(u => u.Rota).Distinct().ToList();
            var todasAsCidades = _userManager.Users.Select(u => u.Cidade).Distinct().ToList();

            ViewData["Instituicoes"] = todasInstituicoes;
            ViewData["Rotas"] = todasAsRotas;
            ViewData["Cidades"] = todasAsCidades;

            return View(SearchUsers);
        }

        public IActionResult FullReport(string? rota, string? cidade, string? instituicao)
        {
            IQueryable<ApplicationUser> query = _userManager.Users;

            if (rota != null && rota.Length > 0)
            {
                query = query.Where(p => p.Rota.Contains(rota));
            }

            if (cidade != null && cidade.Length > 0)
            {
                query = query.Where(p => p.Cidade.Contains(cidade));
            }

            if (instituicao != null && instituicao.Length > 0)
            {
                query = query.Where(p => p.Instituicao.Contains(instituicao));
            }

            var users = query.ToList();

            ViewBag.Users = users;

            var SearchUsers = new SearchUsers()
            {
                Rota = rota,
                Cidade = cidade,
                Instituicao = instituicao,
            };

            var todasInstituicoes = _userManager.Users.Select(u => u.Instituicao).Distinct().ToList();
            var todasAsRotas = _userManager.Users.Select(u => u.Rota).Distinct().ToList();
            var todasAsCidades = _userManager.Users.Select(u => u.Cidade).Distinct().ToList();

            ViewData["Instituicoes"] = todasInstituicoes;
            ViewData["Rotas"] = todasAsRotas;
            ViewData["Cidades"] = todasAsCidades;

            return View(SearchUsers);
        }

        public async Task<IActionResult> DeleteAccount(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser!.Id == appUser.Id)
            {
                return RedirectToAction("Index", "Users", new { id });
            }

            // Delete
            var result = await _userManager.DeleteAsync(appUser);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction("Index", "Users", new { id });
        }
    }
}
