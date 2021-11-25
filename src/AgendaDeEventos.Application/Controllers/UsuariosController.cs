using System;
using System.Linq;
using System.Threading.Tasks;
using AgendaDeEventos.Application.ViewModels.Usuarios;
using AgendaDeEventos.Data.Context;
using AgendaDeEventos.Data.SearchModels;
using AgendaDeEventos.Domain.Interfaces.Repository;
using AgendaDeEventos.Domain.Interfaces.Services;
using AgendaDeEventos.Domain.Interfaces.UnitOfWork;
using AgendaDeEventos.Domain.Models;
using AspNetCore.IQueryable.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeEventos.Application.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class Usuarios : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public Usuarios(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] UsuarioSearch usuarioSearch)
        {
            var usuarios = await _usuarioRepository.Buscar(usuarioSearch);
            return View();
        }
        
        [HttpGet("adicionar")]
        public IActionResult Adicionar()
        {
            return View();
        }
        
        [HttpPost("adicionar")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Adicionar(UsuarioAdicionarViewModel usuarioAdicionarViewModel)
        {
            if (!ModelState.IsValid)
                return View();
            
            var usuario = _mapper.Map<Usuario>(usuarioAdicionarViewModel);
            
            usuario = await _usuarioService.Salvar(usuario);

            return RedirectToAction("Index");
        }
    }
}