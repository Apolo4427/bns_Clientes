using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModuloClientes.Core.Models;

namespace ModuloClientes.Infrastructure.Data.Configurations
{
    public class EmpresaClienteConfig : IEntityTypeConfiguration<EmpresaCliente>
    {
        public void Configure(EntityTypeBuilder<EmpresaCliente> builder)
        {
            builder.ToTable("EmpresaCliente");

            builder.HasKey(ec => new { ec.ClienteId, ec.EmpresaId });

            builder.HasOne(ec => ec.Cliente)
                .WithMany(c => c.Empresas)
                .HasForeignKey(ec => ec.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ec => ec.Empresa)
                .WithMany(e => e.Clientes)
                .HasForeignKey(ec => ec.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ec => ec.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion");;
        }
    }
}