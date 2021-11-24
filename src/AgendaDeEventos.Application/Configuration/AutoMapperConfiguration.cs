using AgendaDeEventos.Application.ViewModels.Usuarios;
using AgendaDeEventos.Domain.Models;
using AutoMapper;

namespace AgendaDeEventos.Application.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Usuario, UsuarioAdicionarViewModel>()
                .ReverseMap();
        }
    }
}