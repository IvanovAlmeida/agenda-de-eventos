using AgendaDeEventos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaDeEventos.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .HasKey(p => p.Id);
            
            builder
                .Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(p => p.Email)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder
                .Property(p => p.Senha)
                .HasColumnType("varchar(255)")
                .IsRequired();
            
            builder
                .Property(p => p.Grupo)
                .HasConversion<int>()
                .IsRequired();
            
            builder
                .Property(p => p.CriadoEm);
            
            builder
                .HasOne(p => p.CriadoPor)
                .WithMany()
                .HasForeignKey(p => p.CriadoPorId)
                .HasPrincipalKey(p => p.Id)
                .IsRequired();
            
            builder
                .Property(p => p.AtualizadoEm);
            
            builder
                .HasOne(p => p.AtualizadoPor)
                .WithMany()
                .HasForeignKey(p => p.AtualizadoPorId)
                .HasPrincipalKey(p => p.Id)
                .IsRequired();
        }
    }
}