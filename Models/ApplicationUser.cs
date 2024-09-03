using Microsoft.AspNetCore.Identity;

namespace CampusConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } = "";
        public string Sobrenome { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string Instituicao { get; set; } = "";
        public string Matricula { get; set; } = "";
        public string Curso { get; set; } = "";
        public string Periodo { get; set; } = "";
    }
}
