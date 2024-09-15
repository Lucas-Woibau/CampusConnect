using CampusConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }
            if (registerDto.Instituicao == "OUTRA" && !string.IsNullOrEmpty(registerDto.NovaInstituicao))
            {
                registerDto.Instituicao = registerDto.NovaInstituicao;
            }
            var user = new ApplicationUser()
            {
                Nome = registerDto.Nome,
                Sobrenome = registerDto.Sobrenome,
                UserName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Telefone,
                Cidade = registerDto.Cidade,
                Rota = registerDto.Rota,
                Instituicao = registerDto.Instituicao,
                Curso = registerDto.Curso,
                Matricula = registerDto.Matricula,
                Periodo = registerDto.Periodo,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "aluno");
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerDto);
        }

        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Senha,
                loginDto.LembrarDeMim, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Tentativa de login inválida.";
            }

            return View(loginDto);
        }

        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var profileDto = new ProfileDto()
            {
                Nome = appUser.Nome,
                Sobrenome = appUser.Sobrenome,
                Email = appUser.Email ?? "",
                Telefone = appUser.PhoneNumber ?? "",
                Cidade = appUser.Cidade,
                Rota = appUser.Rota,
                Instituicao = appUser.Instituicao,
                Curso = appUser.Curso,
                Matricula = appUser.Matricula,
                Periodo = appUser.Periodo,
            };

            return View(profileDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileDto profileDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Por favor preencha todos os campos";
                return View(profileDto);
            }

            var appUser = await _userManager.GetUserAsync(User);

            if (appUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (profileDto.Instituicao == "OUTRA" && !string.IsNullOrEmpty(profileDto.NovaInstituicao))
            {
                profileDto.Instituicao = profileDto.NovaInstituicao;
            }
            else if (profileDto.Instituicao != "OUTRA")
            {
                profileDto.NovaInstituicao = ""; // Limpa o valor da outra instituição se não for selecionada
            }

            appUser.Nome = profileDto.Nome;
            appUser.Sobrenome = profileDto.Sobrenome;
            appUser.UserName = profileDto.Email;
            appUser.Email = profileDto.Email;
            appUser.PhoneNumber = profileDto.Telefone;
            appUser.Cidade = profileDto.Cidade;
            appUser.Instituicao = profileDto.Instituicao;
            appUser.Rota = profileDto.Rota;
            appUser.Curso = profileDto.Curso;
            appUser.Matricula = profileDto.Matricula;
            appUser.Periodo = profileDto.Periodo;

            var result = await _userManager.UpdateAsync(appUser);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Perfil atualizado com sucesso";
            }
            else
            {
                TempData["ErrorMessage"] = "Não foi possível atualizar o perfil: " + result.Errors.First().Description;
            }

            return RedirectToAction("Profile");
        }


        [Authorize]
        public IActionResult Password()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Password(PasswordDto passwordDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ChangePasswordAsync(appUser,
                passwordDto.SenhaAtual, passwordDto.NovaSenha);

            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = "Senha atualizada com sucesso";
            }
            else
            {
                ViewBag.ErrorMessage = "Não foi possível atualizar a senha:" + result.Errors.First().Description;
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword([Required, EmailAddress] string email)
        //{
        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    ViewBag.Email = email;

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.EmailError = ModelState["email"]?.Errors.First().ErrorMessage ?? "Invalid Email Address";
        //        return View();
        //    }

        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user != null)
        //    {
        //        // generate password reset token
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        string resetUrl = Url.ActionLink("ResetPassword", "Account", new { token }) ?? "URL Error";

        //        // send url by email
        //        string senderName = _configuration["BrevoSettings:SenderName"] ?? "";
        //        string senderEmail = _configuration["BrevoSettings:SenderEmail"] ?? "";
        //        string username = user.FirstName + " " + user.LastName;
        //        string subject = "Password Reset";
        //        string message = "Dear " + username + ",\n\n" +
        //                         "You can reset your password using the following link:\n\n" +
        //        resetUrl + "\n\n" +
        //        "Best Regards";

        //        EmailSender.SendEmail(senderName, senderEmail, username, email, subject, message);
        //    }

        //    ViewBag.SuccessMessage = "Please check your Email account and click on the Password Reset link!";

        //    return View();
        //}

        //public IActionResult ResetPassword(string? token)
        //{
        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    if (token == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> ResetPassword(string? token, PasswordResetDto model)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            if (token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Token não é válido.";
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, token, model.Senha);

            if (result.Succeeded)
            {
                ViewBag.SuccessMessage = "Senha atualizada.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
