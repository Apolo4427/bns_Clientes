using ModuloClientes.Core.Models;

namespace ModuloClientes.Core.Ports.Repositories
{
    /// <summary>
    /// Puerto que abstrae el CRUD de Clientes.
    /// </summary>
    public interface IClienteRepository
    {
        Task<Cliente> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<Cliente>> ListAsync(int pageNumber, int pageSize, CancellationToken ct);
        Task AddAsync(Cliente cliente, CancellationToken ct);
        Task UpdateAsync(Cliente cliente, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}