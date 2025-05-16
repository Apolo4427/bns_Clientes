using Microsoft.EntityFrameworkCore;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Repositories;
using ModuloClientes.Infrastructure.Data;

namespace ModuloClientes.Infrastructure.Persistence.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DbClientesContext _context;

        public EmpresaRepository(DbClientesContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existe = await _context.Empresas.FindAsync(id)
                ?? throw new KeyNotFoundException($"La empresa con id {id} no se encuentra");
            _context.Empresas.Remove(existe);
            await _context.SaveChangesAsync();

        }

        public async Task<Empresa> GetByIdAsync(int id)
        {
            var empresa = await _context.Empresas
                .Include(e => e.Clientes)
                    .ThenInclude(ec => ec.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);

            return empresa 
                ?? throw new KeyNotFoundException(
                    $"La empresa con id: {id} no se encontro"
                );
        }

        public async Task<IEnumerable<Empresa>> ListAsync()
        {
            return await _context.Empresas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }
    }
}