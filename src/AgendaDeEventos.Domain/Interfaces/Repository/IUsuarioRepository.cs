using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaDeEventos.Domain.Models;

namespace AgendaDeEventos.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task Salvar(Usuario usuario);
        Task<Usuario> BuscarPorEmail(string email);
    }
}