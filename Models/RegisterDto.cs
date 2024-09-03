using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Preencha seu primeiro nome."), MaxLength(100)]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "Preencha seu sobrenome."), MaxLength(100)]
        public string Sobrenome { get; set; } = "";

        [Phone(ErrorMessage = "O formato do telefone está incorreto"), MaxLength(20)]
        public string Telefone { get; set; } = "";

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [Required, MaxLength(100)]
        public string Cidade { get; set; } = "";

        [Required, MaxLength(100)]
        public string Instituicao { get; set; } = "";

        public string Matricula { get; set; } = "";

        [Required, MaxLength(100)]
        public string Curso { get; set; } = "";

        [Required, MaxLength(100)]
        public string Periodo { get; set; } = "";

        [Required, MaxLength(100)]
        public string Senha { get; set; } = "";

        [Required(ErrorMessage = "A confirmação da senha falhou.")]
        [Compare("Senha", ErrorMessage = "As senhas não se correspondem")]
        public string ConfirmarSenha { get; set; } = "";
    }
}
