using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class PasswordResetDto
    {
        [Required(ErrorMessage ="Digite seu email."), EmailAddress]
        public string Email { get; set; } = "";

        [Required, MaxLength(100)]
        public string Senha { get; set; } = "";

        [Required(ErrorMessage = "O campo de confirmar senha precisa ser preenchido.")]
        [Compare("Senha", ErrorMessage = "As senhas não se correspondem")]
        public string ConfirmarSenha { get; set; } = "";
    }
}
