using System.Linq;
using System.Threading.Tasks;
using AgendaDeEventos.Data.Context;
using AgendaDeEventos.Domain.Interfaces.Repository;
using AgendaDeEventos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeEventos.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataDbContext db) : base(db)
        { }

        public async Task Salvar(Usuario usuario)
        {
            await DbSet.AddAsync(usuario);
        }

        public async Task<Usuario> BuscarPorEmail(string email)
        {
            return await DbSet
                            .AsNoTrackingWithIdentityResolution()
                            .Where(u => u.Email.Equals(email))
                            .FirstOrDefaultAsync();
        }
    }
}