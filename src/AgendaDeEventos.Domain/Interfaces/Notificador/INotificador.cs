using System.Collections.Generic;

namespace AgendaDeEventos.Domain.Interfaces.Notificador
{
    public interface INotificador
    {
        void Notificar(string mensagem);
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
    }
}