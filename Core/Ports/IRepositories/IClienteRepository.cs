using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IUnitOfWorks;

namespace ModuloClientes.Core.Ports.IRepositories
{
    /// <summary>
    /// Puerto que abstrae el CRUD de Clientes.
    /// </summary>
    public interface IClienteRepository : IUnitOfWork
    {
        Task<Cliente> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Cliente> GetByIdAsync(Guid id, bool includeRelations,CancellationToken ct = default);
        Task<IReadOnlyList<Cliente>> ListAsync(int pageNumber, int pageSize, CancellationToken ct= default);
        Task AddAsync(Cliente cliente, CancellationToken ct= default);
        Task UpdateAsync(Cliente cliente, CancellationToken ct= default);
        Task DeleteAsync(Guid id, CancellationToken ct= default);
    }
}