using System.Threading.Tasks;

namespace AgendaDeEventos.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<int> CommitAsync();
    }
}