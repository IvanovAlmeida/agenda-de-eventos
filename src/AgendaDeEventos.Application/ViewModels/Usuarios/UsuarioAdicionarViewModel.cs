using System.ComponentModel.DataAnnotations;
using AgendaDeEventos.Domain.Models;

namespace AgendaDeEventos.Application.Models.Usuarios
{
    public class UsuarioAdicionarViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MinLength(5, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um email válido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório."), DataType(DataType.Password)]
        public string Senha { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório."), EnumDataType(typeof(Grupo))]
        public Grupo Grupo { get; set; }
    }
}