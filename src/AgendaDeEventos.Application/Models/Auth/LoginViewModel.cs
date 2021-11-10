using System.ComponentModel.DataAnnotations;

namespace AgendaDeEventos.Application.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um email válido.")]
        public string Email { get; set; } 
        
        [Required(ErrorMessage = "O campo {0} é obrigatório."), DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}