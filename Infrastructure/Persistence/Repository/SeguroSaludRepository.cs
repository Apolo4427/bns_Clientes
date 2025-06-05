using System.Data;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IRepositories;
using ModuloClientes.Infrastructure.Data;

namespace ModuloClientes.Infrastructure.Persistence.Repository
{
    public class SeguroSaludRepository : ISeguroSaludRepository
    {
        private readonly DbClientesContext _dbContext;

        public SeguroSaludRepository(DbClientesContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(SeguroSalud seguroSalud, CancellationToken ct = default)
        {
            await _dbContext.SegurosSalud.AddAsync(seguroSalud, ct);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var seguroSalud = await _dbContext.SegurosSalud.FindAsync(
                keyValues: new object[] { id },
                cancellationToken: ct
            ) ?? throw new KeyNotFoundException(
                $"El seguro de salud con id : {id} no ha sido encontrado"
            );
            _dbContext.SegurosSalud.Remove(seguroSalud);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<SeguroSalud> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var seguro = await _dbContext.SegurosSalud
                .AsNoTracking()
                .Include(s => s.Clientes)
                .FirstOrDefaultAsync(s => s.Id == id, ct)
                    ?? throw new KeyNotFoundException(
                        $"El seguro de id {id} no ha sido encontrado"
                    );
            return seguro;
        }



        public async Task<IReadOnlyList<SeguroSalud>> ListAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var skip = (pageNumber - 1) * pageSize;
            return await _dbContext.SegurosSalud
                            .AsNoTracking()
                            .Skip(skip)
                            .Take(pageSize)
                            .ToListAsync(ct);
        }

        public async Task UpdateAsync(SeguroSalud seguroSalud, CancellationToken ct = default)
        {
            try
            {
                _dbContext.Entry(seguroSalud).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var dataBaseValues = await entry.GetDatabaseValuesAsync();

                if (dataBaseValues == null)
                    throw new DbUpdateConcurrencyException(
                        "El seguro de salud ha sido eliminado por otro usuario. Por favor cree un nuevo seguro",
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
                    $"El seguro de salud fue modificado por otro usuario. " +
                    $"Campos cambiados: {string.Join(", ", changedProperties)}. " +
                    "Por favor actualice la página y vuelva a intentar.",
                    ex
                );
            }
            catch (Exception ex)
            {
                throw new DataException("Error al intentar modificar los datos de el seguro: ", ex);
            }
        }
    }
}