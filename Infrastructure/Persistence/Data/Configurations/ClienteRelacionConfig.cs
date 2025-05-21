using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModuloClientes.Core.Models;

namespace ModuloClientes.Infrastructure.Data.Configurations
{
    public class ClienteRelacionConfig : IEntityTypeConfiguration<ClienteRelacion>
    {
        public void Configure(EntityTypeBuilder<ClienteRelacion> b)
        {
            b.ToTable("ClienteRelacion");

            // clave compuesta
            b.HasKey(cr => new { cr.ClienteId, cr.RelacionadoId });

            // relaciones
            b.HasOne(cr => cr.Cliente)
             .WithMany(c => c.Relaciones)
             .HasForeignKey(cr => cr.ClienteId)
             
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(cr => cr.Relacionado)
             .WithMany()
             .HasForeignKey(cr => cr.RelacionadoId)
             .OnDelete(DeleteBehavior.Restrict);

            // token de concurrencia
            b.Property(cr => cr.RowVersion)
             .IsRowVersion()
             .IsConcurrencyToken();
        }
    }
}