using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class PasswordDto
    {
        [Required(ErrorMessage = "Preencha a senha atual."), MaxLength(100)]
        public string SenhaAtual { get; set; } = "";

        [Required(ErrorMessage = "Preencha a nova senha."), MaxLength(100)]
        public string NovaSenha { get; set; } = "";

        [Required(ErrorMessage = "Preencha a confirmação da nova senha")]
        [Compare("NovaSenha", ErrorMessage = "Os campos não se correspondem")]
        public string ConfirmarSenha { get; set; } = "";
    }
}
