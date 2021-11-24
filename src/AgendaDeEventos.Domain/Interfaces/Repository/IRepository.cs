using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgendaDeEventos.Domain.Models;
using AspNetCore.IQueryable.Extensions;

namespace AgendaDeEventos.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entidade
    {
        Task<IEnumerable<Entidade>> Buscar(ICustomQueryable search);
        Task<IEnumerable<Entidade>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<Entidade> BuscarPorId(int id);
        Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}