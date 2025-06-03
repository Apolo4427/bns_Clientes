using ModuloClientes.Core.Models;

namespace ModuloClientes.Core.Ports.IRepositories
{
    public interface ISeguroSaludRepository
    {
        Task<SeguroSalud> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<SeguroSalud>> ListAsync(int pageNumber, int pageSize, CancellationToken ct= default);
        Task AddAsync(SeguroSalud seguroSalud, CancellationToken ct= default);
        Task UpdateAsync(SeguroSalud seguroSalud, CancellationToken ct= default);
        Task DeleteAsync(Guid id, CancellationToken ct= default);
    }
}