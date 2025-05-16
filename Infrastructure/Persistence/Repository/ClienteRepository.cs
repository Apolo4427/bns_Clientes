using Microsoft.EntityFrameworkCore;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Repositories;
using ModuloClientes.Infrastructure.Data;

namespace ModuloClientes.Infrastructure.Persistence.Repository
{
    public class ClienteRepository : IClienteRepository
    {

        private readonly DbClientesContext _context;

        public ClienteRepository(DbClientesContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existente = await _context.Clientes.FindAsync(id)
                ?? throw new KeyNotFoundException($"El cliente con el id {id} no existe");
            _context.Clientes.Remove(existente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Empresas)
                    .ThenInclude(ec => ec.Empresa)
                .Include(c => c.Relaciones)
                    .ThenInclude(r => r.Relacionado)
                .FirstOrDefaultAsync(c => c.Id == id);
   
            return cliente
                ?? throw new KeyNotFoundException($"El cliente con el id {id} no se encontro");
        }

        public async Task<IEnumerable<Cliente>> ListAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            return await _context.Clientes
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }
    }
}