using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgendaDeEventos.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeEventos.Data.Context
{
    public class DataDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public DataDbContext(DbContextOptions<DataDbContext> options, IHttpContextAccessor httpContext): base(options)
        {
            _httpContext = httpContext;
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureDefaultColumns(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var claim = _httpContext?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.Equals("id"));
            var userId = int.Parse(claim?.Value);
            
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entidade && e.State is EntityState.Added or EntityState.Modified);
            
            foreach (var entityEntry in entries)
            {
                ((Entidade)entityEntry.Entity).AtualizadoEm = DateTime.Now;
                ((Entidade)entityEntry.Entity).AtualizadoPorId = userId;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entidade)entityEntry.Entity).CriadoEm = DateTime.Now;
                    ((Entidade)entityEntry.Entity).CriadoPorId = userId;
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ConfigureDefaultColumns(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model
                                                                .GetEntityTypes()
                                                                .SelectMany(e => e.GetProperties())
                                                                .Where(p => p.ClrType == typeof(string));

            foreach (var property in properties)
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);

            var foreignKeys = modelBuilder.Model
                                                                    .GetEntityTypes()
                                                                    .SelectMany(e => e.GetForeignKeys());

            foreach (var relationship in foreignKeys)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}