using ModuloClientes.Core.Models;

namespace ModuloClientes.Core.Ports.Repositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa> GetByIdAsync(Guid id);
        Task<IEnumerable<Empresa>> ListAsync(int pageNumber, int pageSize);
        Task AddAsync(Empresa empresa);
        Task UpdateAsync(Empresa empresa);
        Task DeleteAsync(Guid id);
    }
}