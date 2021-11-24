using System.Threading.Tasks;
using AgendaDeEventos.Domain.Models;

namespace AgendaDeEventos.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Salvar(Usuario usuario);
    }
}