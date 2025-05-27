using ModuloClientes.Core.Models;

namespace ModuloClientes.Core.Ports.Repositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa> GetByIdAsync(Guid id);
        Task<IEnumerable<Empresa>> ListAsync(int pageNumber, int pageSize);
        Task AddAsync(Empresa empresa);
        Task UpdateAsync(Empresa empresa, CancellationToken ct);
        Task DeleteAsync(Guid id);
    }
}