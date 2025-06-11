using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModuloClientes.Core.Models;

namespace ModuloClientes.Infrastructure.Data.Configurations
{
    public class EmpresaConfig : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

            // Guid secuencial 
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(e => e.FechaConstitucion)
                   .IsRequired();

            // Owned Types
            builder.OwnsOne(e => e.Nombre, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Nombre")
                  .IsRequired()
                  .HasMaxLength(200);
            });

            builder.OwnsOne(e => e.Ein, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("EIN")
                  .IsRequired()
                  .HasMaxLength(9);
            });

            builder.OwnsOne(e => e.Direccion, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Direccion")
                  .IsRequired()
                  .HasMaxLength(200);
            });

            builder.OwnsOne(e => e.Telefono, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Telefono")
                  .IsRequired()
                  .HasMaxLength(15);
            });

            builder.OwnsOne(e => e.CorreoContacto, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("EmailContacto")
                  .IsRequired()
                  .HasMaxLength(254);
            });

            // Relación con EmpresaCliente
            builder.HasMany(e => e.Clientes)
                   .WithOne(ec => ec.Empresa)
                   .HasForeignKey(ec => ec.EmpresaId);

            // Índice único sobre EIN (valor interno)
            builder.HasIndex(e => e.Ein.Value)
                   .IsUnique();

            // Token de concurrencia
            builder.Property(c => c.RowVersion)
               .IsRowVersion();
        }
    }
}