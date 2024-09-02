using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class ProfileDto
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

        [Required, MaxLength(100)]
        public string Matricula { get; set; } = "";

        [Required, MaxLength(100)]
        public string Curso { get; set; } = "";

        [Required, MaxLength(100)]
        public string Periodo { get; set; } = "";
    }
}
