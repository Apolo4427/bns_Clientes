using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.Infrastructure.Data.Configurations
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
             builder.ToTable("Clientes");

            // PK, propiedades escalares, índices, etc.
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FechaNacimiento).IsRequired();

            // token de concurrencia
            builder.Property(cr => cr.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            // --- Aquí van los Owned Types ---
            builder.OwnsOne(c => c.Nombre, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Nombre")
                  .IsRequired()
                  .HasMaxLength(100);
            });

            builder.OwnsOne(c => c.Apellido, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Apellido")
                  .IsRequired()
                  .HasMaxLength(100);
            });

            builder.OwnsOne(c => c.Correo, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Email")
                  .IsRequired()
                  .HasMaxLength(254);
            });

            builder.OwnsOne(c => c.Telefono, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Telefono")
                  .IsRequired()
                  .HasMaxLength(15);
            });

            builder.OwnsOne(c => c.Direccion, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Direccion")
                  .IsRequired()
                  .HasMaxLength(200);
            });

            builder.OwnsOne(c => c.SocialSecurityNumber, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("SSN")
                  .IsRequired()
                  .HasMaxLength(11);
            });

            builder.OwnsMany(c => c.Oficios, vo =>
            {
              vo.WithOwner().HasForeignKey("ClienteId");
              vo.HasKey("ClienteId", nameof(Oficio.Value));
              vo.Property(p => p.Value).HasColumnName("Oficio").IsRequired().HasMaxLength(100);
              vo.ToTable("ClienteOficios");
            });
        }
    }
}