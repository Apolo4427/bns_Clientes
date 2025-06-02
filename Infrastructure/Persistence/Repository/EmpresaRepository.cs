using System.Data;
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
        public async Task AddAsync(Empresa empresa, CancellationToken ct)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var existe = await _context.Empresas.FindAsync(id, ct)
                ?? throw new KeyNotFoundException($"La empresa con id {id} no se encuentra");
            _context.Empresas.Remove(existe);
            await _context.SaveChangesAsync(ct);

        }

        public async Task<Empresa> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var empresa = await _context.Empresas
                .Include(e => e.Clientes)
                    .ThenInclude(ec => ec.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            return empresa 
                ?? throw new KeyNotFoundException(
                    $"La empresa con id: {id} no se encontro"
                );
        }

        public async Task<IReadOnlyList<Empresa>> ListAsync(int pageNumber, int pageSize, CancellationToken ct)
        {
            var skip = (pageNumber - 1) * pageSize;
            return await _context.Empresas
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task UpdateAsync(Empresa empresa, CancellationToken ct)
        {
            try
            {
                _context.Entry(empresa).State = EntityState.Modified;
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
                var databaseEntity = (Empresa)dataBaseValues.ToObject(); // obtenemos el objeto y lo casteamos a empresa

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