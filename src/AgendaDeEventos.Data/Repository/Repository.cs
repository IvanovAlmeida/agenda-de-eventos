using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgendaDeEventos.Data.Context;
using AgendaDeEventos.Domain.Interfaces.Repository;
using AgendaDeEventos.Domain.Models;
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeEventos.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entidade, new()
    {
        protected readonly DataDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DataDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();;
        }

        public async Task<IEnumerable<Entidade>> Buscar(ICustomQueryable searchEntidade)
        {
            return await DbSet
                            .AsNoTrackingWithIdentityResolution()
                            .AsQueryable()
                            .Apply(searchEntidade).ToListAsync();
        }
        
        public async Task<IEnumerable<Entidade>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet
                            .AsNoTrackingWithIdentityResolution()
                            .Where(predicate)
                            .ToListAsync();
        }
        
        public async Task<Entidade> BuscarPorId(int id)
        {
            return await DbSet
                            .AsNoTrackingWithIdentityResolution()
                            .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            var search = DbSet.AsNoTrackingWithIdentityResolution();

            if (predicate != null)
            {
                search = search.Where(predicate);
            }

            return await search.CountAsync();
        }
        
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}