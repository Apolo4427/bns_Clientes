using System.Data;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IRepositories;
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
                keyValues: new object[] { id },
                cancellationToken: ct
            )
                ?? throw new KeyNotFoundException($"El cliente con el id {id} no existe");
            _context.Clientes.Remove(existente);
            await _context.SaveChangesAsync(ct);
            
        }

        public async Task<Cliente> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Empresas)
                    .ThenInclude(ec => ec.Empresa)
                .Include(c => c.Relaciones)
                    .ThenInclude(r => r.Relacionado)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
   
            return cliente
                ?? throw new KeyNotFoundException($"El cliente con el id {id} no se encontro");
        }

        public async Task<Cliente> GetByIdAsync(Guid id, bool includeRelations, CancellationToken ct = default)
        {
            if (includeRelations == true)
            {
                return await _context.Clientes
                                .Include(c => c.Relaciones)
                                .FirstOrDefaultAsync(c => c.Id == id, ct)
                                    ?? throw new KeyNotFoundException(
                                        $"El cliente con id {id} no ha sido encontrado"
                                    );
            }
            return await _context.Clientes
                            .FindAsync(id)
                                ?? throw new KeyNotFoundException(
                                        $"El cliente con id {id} no ha sido encontrado"
                                    );
        }

        public async Task<IReadOnlyList<Cliente>> ListAsync(
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
                    throw new DbUpdateConcurrencyException(
                        "El cliente ha sido eliminado por otro usuario. Por favor cree un nuevo cliente",
                        ex
                    );

                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;

                var changedProperties = new List<string>();
                foreach (var propertie in entry.OriginalValues.Properties)
                {
                    var originalValue = originalValues[propertie];
                    var currentValue = currentValues[propertie];

                    if (!Equals(originalValue, currentValue))
                        changedProperties.Add(propertie.Name);
                }

                throw new DbUpdateConcurrencyException(
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