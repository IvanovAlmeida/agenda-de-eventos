using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgendaDeEventos.Application.Models.Auth;
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
        private readonly List<Usuario> _usuarios;

        public AuthController()
        {
            _usuarios = CriarUsuarios();
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

            var usuario = BuscarUsuarioPorEmail(loginViewModel.Email);
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
        private Usuario BuscarUsuarioPorEmail(string email) 
            => _usuarios.FirstOrDefault(u => u.Email.Equals(email));

        [NonAction]
        private bool VerificarUsuario(Usuario usuario, string senha)
        {
            var hasher = new PasswordHasher<Usuario>();
            var result = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
            return result == PasswordVerificationResult.Success;
        }
        
        [NonAction]
        private List<Usuario> CriarUsuarios()
        {
            var hasher = new PasswordHasher<Usuario>();
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "Usuário 1",
                    Email = "usuario1@email.com"
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Usuário 2",
                    Email = "usuario2@email.com"
                },
                new Usuario
                {
                    Id = 3,
                    Nome = "Usuário 3",
                    Email = "usuario3@email.com"
                },
                new Usuario
                {
                    Id = 4,
                    Nome = "Usuário 4",
                    Email = "usuario4@email.com"
                },
                new Usuario
                {
                    Id = 5,
                    Nome = "Usuário 5",
                    Email = "usuario5@email.com"
                },
            };

            usuarios.ForEach(u => u.Senha = hasher.HashPassword(u, u.Email));
            return usuarios;
        }
    }
}