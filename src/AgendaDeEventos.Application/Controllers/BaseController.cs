using System.ComponentModel.DataAnnotations;
using AgendaDeEventos.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;

namespace AgendaDeEventos.Application.Controllers
{
    [Controller]
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Notificar(mensagem);
        }
        
        protected bool TemNotificacao()
        {
            return _notificador.TemNotificacao();
        }

        public override ViewResult View()
        {
            AddNotificacoesEmModelState();
            return base.View();
        }

        public override ViewResult View(string viewName)
        {
            AddNotificacoesEmModelState();
            return base.View(viewName);
        }

        public override ViewResult View(string viewName, object model)
        {
            AddNotificacoesEmModelState();
            return base.View(viewName, model);
        }

        private void AddNotificacoesEmModelState()
        {
            if (!TemNotificacao())
                return;

            ModelState.Clear();
            
            _notificador
                    .ObterNotificacoes()
                    .ForEach(n => ModelState.AddModelError("", n.Mensagem));
        }
    }
}