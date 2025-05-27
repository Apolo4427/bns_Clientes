using System.Data;
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

        public async Task AddAsync(Cliente cliente, CancellationToken ct)
        {
            await _context.Clientes.AddAsync(cliente,ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var existente = await _context.Clientes.FindAsync(
                new[] {id},
                ct
            )
                ?? throw new KeyNotFoundException($"El cliente con el id {id} no existe");
            _context.Clientes.Remove(existente);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Cliente> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Empresas)
                    .ThenInclude(ec => ec.Empresa)
                .Include(c => c.Relaciones)
                    .ThenInclude(r => r.Relacionado)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
   
            return cliente
                ?? throw new KeyNotFoundException($"El cliente con el id {id} no se encontro");
        }

        public async Task<IEnumerable<Cliente>> ListAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct
        )
        {
            var skip = (pageNumber - 1) * pageSize;
            return await _context.Clientes
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task UpdateAsync(Cliente cliente, CancellationToken ct)
        {
            try
            {
                _context.Entry(cliente).State = EntityState.Modified;
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var dataBaseValues = await entry.GetDatabaseValuesAsync();

                if (dataBaseValues == null)
                    throw new DBConcurrencyException(
                        "El cliente ha sido eliminado por otro usuario. Por favor cree un nuevo cliente",
                        ex
                    );

                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                var databaseEntity = (Cliente)dataBaseValues.ToObject();

                var changedProperties = new List<string>();
                foreach (var propertie in entry.OriginalValues.Properties)
                {
                    var originalValue = originalValues[propertie];
                    var currentValue = currentValues[propertie];

                    if (!Equals(originalValue, currentValue))
                        changedProperties.Add(propertie.Name);
                }

                throw new DBConcurrencyException(
                    $"El cliente fue modificado por otro usuario. " +
                    $"Campos cambiados: {string.Join(", ", changedProperties)}. " +
                    "Por favor actualice la página y vuelva a intentar.",
                    ex
                );
            }
            catch (Exception ex)
            {
                throw new DataException("Error al guardar los cambios del cliente.", ex);
            }
        }
    }
}