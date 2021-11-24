using System.Threading.Tasks;
using AgendaDeEventos.Data.Context;
using AgendaDeEventos.Domain.Interfaces.UnitOfWork;

namespace AgendaDeEventos.Data.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DataDbContext _context;

        public UnitOfWork(DataDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}