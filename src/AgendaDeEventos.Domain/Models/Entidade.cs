using System;

namespace AgendaDeEventos.Domain.Models
{
    public abstract class Entidade
    {
        public int Id { get; set; }
        
        public DateTime CriadoEm { get; set; }
        public int CriadoPorId { get; set; }
        public virtual Usuario CriadoPor { get; set; }
        
        public DateTime AtualizadoEm { get; set; }
        public int AtualizadoPorId { get; set; }
        public virtual Usuario AtualizadoPor { get; set; }
    }
}