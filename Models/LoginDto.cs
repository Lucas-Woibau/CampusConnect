using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Digite seu email."), EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Digite sua senha."), MaxLength(100)]
        public string Senha { get; set; } = "";
        public bool LembrarDeMim { get; set; }
    }
}
