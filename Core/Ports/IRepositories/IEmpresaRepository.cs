using ModuloClientes.Core.Models;

namespace ModuloClientes.Core.Ports.Repositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Empresa>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task AddAsync(Empresa empresa , CancellationToken cancellationToken = default);
        Task UpdateAsync(Empresa empresa, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}