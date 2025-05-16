using ModuloClientes.Core.Models;

namespace ModuloClientes.Core.Ports.Repositories
{
    /// <summary>
    /// Puerto que abstrae el CRUD de Clientes.
    /// </summary>
    public interface IClienteRepository
    {
        Task<Cliente> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> ListAsync(int pageNumber, int pageSize);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}