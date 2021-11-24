using AgendaDeEventos.Data.Context;
using AgendaDeEventos.Data.Repository;
using AgendaDeEventos.Data.UnitOfWork;
using AgendaDeEventos.Domain.Interfaces.Repository;
using AgendaDeEventos.Domain.Interfaces.Services;
using AgendaDeEventos.Domain.Interfaces.UnitOfWork;
using AgendaDeEventos.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaDeEventos.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependenciesInjections(this IServiceCollection services)
        {
            services.AddScoped<DataDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            
            return services;
        }
    }
}