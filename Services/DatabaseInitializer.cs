using CampusConnect.Models;
using Microsoft.AspNetCore.Identity;

namespace CampusConnect.Services
{
    public class DatabaseInitializer
    {
        public static async Task SeedDataAscync(UserManager<ApplicationUser>? userManager,
            RoleManager<IdentityRole>? roleManager)
        {
            if(userManager == null || roleManager == null)
            {
                Console.WriteLine("UserManager ou RoleManager é nullo => Sair");
                return;
            }

            //Verifica se as role existem
            var exists = await roleManager.RoleExistsAsync("admin");
            if (!exists)
            {
                Console.WriteLine("A role admin não existe e está sendo criada");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            exists = await roleManager.RoleExistsAsync("aluno");
            if (!exists)
            {
                Console.WriteLine("A role aluno não existe e está sendo criada");
                await roleManager.CreateAsync(new IdentityRole("aluno"));
            }

            var user = new ApplicationUser()
            {
                Nome = "Administrador",
                Sobrenome = "",
                UserName = "TheAdmin@admin.com",
                Email = "TheAdmin@admin.com",
            };

            string initialPassword = "O@dmin40028922";

            var result = await userManager.CreateAsync(user, initialPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
                Console.WriteLine("Usuário administrador criado com sucesso!");
                Console.WriteLine("Email: " + user.Email);
                Console.WriteLine("Senha inicial: " + initialPassword);
            }
        }
    }
}
