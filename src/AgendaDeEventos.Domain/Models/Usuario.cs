namespace AgendaDeEventos.Domain.Models
{
    public class Usuario: Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Grupo Grupo { get; set; } 
    }
}