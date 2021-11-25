namespace AgendaDeEventos.Domain.Interfaces.Notificador
{
    public class Notificacao
    {
        public string Mensagem { get; private set; }

        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}