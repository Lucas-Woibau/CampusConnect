using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Digite seu primeiro nome."), MaxLength(100)]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "Digite seu sobrenome."), MaxLength(100)]
        public string Sobrenome { get; set; } = "";

        [Phone(ErrorMessage = "O formato do telefone está incorreto"), MaxLength(20)]
        public string Telefone { get; set; } = "";

        [Required(ErrorMessage = "Digite seu email."), EmailAddress,  MaxLength(100)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Digite a cidade mais próxima."), MaxLength(100)]
        public string Cidade { get; set; } = "";

        [Required(ErrorMessage = "Escolha a rota."), MaxLength(100)]
        public string Rota { get; set; } = "";

        [Required(ErrorMessage = "Digite sua instituição de ensino."), MaxLength(100)]
        public string Instituicao { get; set; } = "";

        public string NovaInstituicao { get; set; } = "";

        public string Matricula { get; set; } = "";

        [Required(ErrorMessage = "Digite o seu curso."), MaxLength(100)]
        public string Curso { get; set; } = "";

        public string Periodo { get; set; } = "";

        [Required, MaxLength(100)]
        public string Senha { get; set; } = "";

        [Required(ErrorMessage = "A confirmação da senha falhou.")]
        [Compare("Senha", ErrorMessage = "As senhas não se correspondem")]
        public string ConfirmarSenha { get; set; } = "";
    }
}
