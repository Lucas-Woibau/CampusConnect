using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Senha { get; set; } = "";
        public bool LembrarDeMim { get; set; }
    }
}
