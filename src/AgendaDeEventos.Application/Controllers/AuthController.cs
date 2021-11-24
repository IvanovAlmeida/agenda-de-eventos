using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgendaDeEventos.Application.ViewModels.Auth;
using AgendaDeEventos.Domain.Interfaces.Repository;
using AgendaDeEventos.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgendaDeEventos.Application.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        
        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            ViewData["Title"] = "Login";
            
            if (!ModelState.IsValid)
                return View();

            var usuario = await _usuarioRepository.BuscarPorEmail(loginViewModel.Email);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário ou Senha inválidos!");
                return View();
            }
            
            if (!VerificarUsuario(usuario, loginViewModel.Senha))
            {
                ModelState.AddModelError("", "Usuário ou Senha inválidos!");
                return View();
            }

            await SingInUsuario(usuario, loginViewModel.LembrarMe);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        
        [NonAction]
        private async Task SingInUsuario(Usuario usuario, bool lembrarMe = false)
        {
            var claims = new List<Claim>
            {
                new("id", usuario.Id.ToString()),
                new(ClaimTypes.Name, usuario.Nome),
                new(ClaimTypes.Email, usuario.Email),
                new(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            // TODO: Move ExpiresUtc to appsettings
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2),
                IsPersistent = lembrarMe,
                IssuedUtc = DateTimeOffset.Now
            };
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        
        [NonAction]
        private bool VerificarUsuario(Usuario usuario, string senha)
        {
            var hasher = new PasswordHasher<Usuario>();
            var result = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
            return result == PasswordVerificationResult.Success;
        }
    }
}