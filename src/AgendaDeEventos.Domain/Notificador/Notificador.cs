using System.Collections.Generic;
using System.Linq;
using AgendaDeEventos.Domain.Interfaces.Notificador;

namespace AgendaDeEventos.Domain.Notificador
{
    public class Notificador: INotificador
    {
        private readonly List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Notificar(string mensagem) => _notificacoes.Add(new Notificacao(mensagem));

        public bool TemNotificacao() => _notificacoes.Any();

        public List<Notificacao> ObterNotificacoes() => _notificacoes;
    }
}