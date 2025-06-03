using System.ClientModel.Primitives;
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.Core.Models;
using ModuloClientes.Infrastructure.Data.Configurations;

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
            // base.OnModelCreating(modelBuilder); // aun no es necesario 

            // Aplicar configuraciones para EmpresaCliente
            modelBuilder.ApplyConfiguration(new EmpresaClienteConfig());

            // Aplicar configuraciones para ClienteRelacion
            modelBuilder.ApplyConfiguration(new ClienteRelacionConfig());

            // Aplicar configuraciones para Cliente
            modelBuilder.ApplyConfiguration(new ClienteConfig());

            // Aplicar configuracion para Empleado
            modelBuilder.ApplyConfiguration(new EmpresaConfig());

            // Aplicat Configuracion para SeguroSalud
            modelBuilder.ApplyConfiguration(new SeguroSaludConfig());
        }
    }
}
