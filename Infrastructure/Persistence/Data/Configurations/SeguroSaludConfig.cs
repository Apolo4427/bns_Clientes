using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModuloClientes.Core.Models;

namespace ModuloClientes.Infrastructure.Data.Configurations
{
    public class SeguroSaludConfig : IEntityTypeConfiguration<SeguroSalud>
    {
        public void Configure(EntityTypeBuilder<SeguroSalud> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("SegurosSalud");

            // 2. Clave primaria
            builder.HasKey(s => s.Id);

            // 3. Configuración de la columna Id como GUID secuencial
            builder.Property(s => s.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWSEQUENTIALID()") // Genera GUID secuencial en SQL Server / Azure SQL
                   .ValueGeneratedOnAdd();

            // 4. Propiedades escalares
            builder.Property(s => s.FechaInicio)
                   .IsRequired();

            builder.Property(s => s.FechaFin)
                   .IsRequired();

            builder.Property(s => s.PrimaMensual)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // 5. Value Object: CompanyName (Proveedor)
            //    Mapea CompanyName.Value a la columna “Proveedor” (nvarchar(200))
            builder.OwnsOne(s => s.Proveedor, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Proveedor")
                  .IsRequired()
                  .HasMaxLength(200);
            });

            // 6. Value Object: PlanName (NombrePlan)
            //    Mapea PlanName.Value a la columna “NombrePlan” (nvarchar(100))
            builder.OwnsOne(s => s.NombrePlan, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("NombrePlan")
                  .IsRequired()
                  .HasMaxLength(100);
            });

            // 7. Value Object: PolicyNumber (NumeroPoliza)
            //    Mapea PolicyNumber.Value a la columna “NumeroPoliza” (nvarchar(50))
            builder.OwnsOne(s => s.NumeroPoliza, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("NumeroPoliza")
                  .IsRequired()
                  .HasMaxLength(50);
            });

            builder.HasMany(s => s.Clientes)
                   .WithOne(c => c.SeguroSalud)
                   .HasForeignKey(c => c.SeguroSaludId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
