using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class ProfileDto
    {
        [Required(ErrorMessage = "Preencha seu primeiro nome."), MaxLength(100)]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "Preencha seu sobrenome."), MaxLength(100)]
        public string Sobrenome { get; set; } = "";


        [Required(ErrorMessage = "Digite seu telefone."), MaxLength(20)]
        [RegularExpression(@"\(?\d{2}\)?\s?\d{4,5}-?\d{4}", ErrorMessage = "Insira um telefone válido no formato (XX) XXXXX-XXXX")]
        public string Telefone { get; set; } = "";

        [Required (ErrorMessage = "Preencha seu email."), EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Preencha sua cidade."), MaxLength(100)]
        public string Cidade { get; set; } = "";

        [Required(ErrorMessage = "Preencha sua rota."), MaxLength(100)]
        public string Rota { get; set; } = "";

        [Required(ErrorMessage = "Preencha sua instituição."), MaxLength(100)]
        public string Instituicao { get; set; } = "";
        public string NovaInstituicao { get; set; } = "";
        public string Matricula { get; set; } = "";

        [Required(ErrorMessage = "Preencha seu curso."), MaxLength(100)]
        public string Curso { get; set; } = "";

        [RegularExpression(@"\d+/\d+", ErrorMessage = "Insira um período no formato X/X")]
        public string Periodo { get; set; } = "";
    }
}
