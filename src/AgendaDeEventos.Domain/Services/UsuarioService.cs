using System;
using System.Linq;
using System.Threading.Tasks;
using AgendaDeEventos.Domain.Interfaces.Notificador;
using AgendaDeEventos.Domain.Interfaces.Repository;
using AgendaDeEventos.Domain.Interfaces.Services;
using AgendaDeEventos.Domain.Interfaces.UnitOfWork;
using AgendaDeEventos.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AgendaDeEventos.Domain.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificador _notificador;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, INotificador notificador)
        {
            _unitOfWork = unitOfWork;
            _notificador = notificador;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
            var usuarios = await _usuarioRepository.Buscar(u => u.Email.Equals(usuario.Email));
            if (usuarios.Any())
            {
                _notificador.Notificar("Já existe um usuário com esse Email!");
                return null;
            }

            var hasher = new PasswordHasher<Usuario>();
            usuario.Senha = hasher.HashPassword(usuario, usuario.Senha);
            
            await _usuarioRepository.Salvar(usuario);

            await _unitOfWork.CommitAsync();

            return usuario;
        }
    }
}