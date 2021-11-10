using AgendaDeEventos.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaDeEventos.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependenciesInjections(this IServiceCollection services)
        {
            return services;
        }
    }
}