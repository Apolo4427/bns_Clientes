using Microsoft.EntityFrameworkCore;
using ModuloClientes.Core.Models;

namespace ModuloClientes.Infrastructure.Data
{
    public class DbClientesContext : DbContext
    {
        public DbClientesContext(DbContextOptions<DbClientesContext> options) : base(options){}

        public DbSet<Cliente> Clientes { get; set;} = null!;
        public DbSet<Empresa> Empresas { get; set; } = null!;
        public DbSet<EmpresaCliente> EmpresaClientes { get; set; } = null!;
        public DbSet<SeguroSalud> SegurosSalud { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar clave compuesta en la tabla intermedia
            modelBuilder.Entity<EmpresaCliente>()
                .HasKey(ec => new { ec.ClienteId, ec.EmpresaId });

            // Relación Cliente ↔ EmpresaCliente (1:N)
            modelBuilder.Entity<EmpresaCliente>()
                .HasOne(ec => ec.Cliente)
                .WithMany(c => c.Empresas)
                .HasForeignKey(ec => ec.ClienteId);

            // Relación Empresa ↔ EmpresaCliente (1:N)
            modelBuilder.Entity<EmpresaCliente>()
                .HasOne(ec => ec.Empresa)
                .WithMany(e => e.Clientes)
                .HasForeignKey(ec => ec.EmpresaId);

            // Relación SeguroSalud ↔ Cliente (1:N)
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.SeguroSalud)
                .WithMany(s => s.Clientes)
                .HasForeignKey(c => c.SeguroSaludId);
                
            // Precicion del decimal en SeguroSalud
            modelBuilder.Entity<SeguroSalud>()
                .Property(S => S.PrimaMensual)
                .HasPrecision(18, 2);
        }
    }
}
